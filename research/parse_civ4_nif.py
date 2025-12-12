#!/usr/bin/env python3
"""
Utility helpers for parsing the 20.0.0.4 Civ 4 NIF variant.
Only the block types used by Svart_Monk.nif are implemented.
"""
from __future__ import annotations

from dataclasses import dataclass
from pathlib import Path
from typing import Any, Dict, List, Optional, Tuple
import struct


def _read_lf_string(data: bytes, offset: int) -> Tuple[str, int]:
    end = data.index(b"\n", offset)
    value = data[offset:end].decode("ascii")
    return value, end + 1


@dataclass
class Header:
    header_string: str
    version: int
    endian: int
    user_version: int
    num_blocks: int
    num_block_types: int
    block_types: List[str]
    block_type_indices: List[int]
    unknown: int
    offset: int


class Reader:
    def __init__(self, data: bytes, offset: int = 0) -> None:
        self.data = data
        self.offset = offset

    def tell(self) -> int:
        return self.offset

    def read_bytes(self, count: int) -> bytes:
        start = self.offset
        end = start + count
        chunk = self.data[start:end]
        if len(chunk) != count:
            raise EOFError("Unexpected EOF")
        self.offset = end
        return chunk

    def read_u8(self) -> int:
        (value,) = struct.unpack_from("<B", self.data, self.offset)
        self.offset += 1
        return value

    def read_u16(self) -> int:
        (value,) = struct.unpack_from("<H", self.data, self.offset)
        self.offset += 2
        return value

    def read_u32(self) -> int:
        (value,) = struct.unpack_from("<I", self.data, self.offset)
        self.offset += 4
        return value

    def read_i32(self) -> int:
        (value,) = struct.unpack_from("<i", self.data, self.offset)
        self.offset += 4
        return value

    def read_f32(self) -> float:
        (value,) = struct.unpack_from("<f", self.data, self.offset)
        self.offset += 4
        return value

    def read_string(self) -> str:
        length = self.read_u32()
        raw = self.read_bytes(length)
        return raw.decode("latin1")

    def read_vector3(self) -> Tuple[float, float, float]:
        return (self.read_f32(), self.read_f32(), self.read_f32())

    def read_matrix33(self) -> Tuple[float, ...]:
        return tuple(self.read_f32() for _ in range(9))

    def read_matrix22(self) -> Tuple[float, ...]:
        return tuple(self.read_f32() for _ in range(4))

    def read_color3(self) -> Tuple[float, float, float]:
        return self.read_vector3()

    def read_color4(self) -> Tuple[float, float, float, float]:
        return (self.read_f32(), self.read_f32(), self.read_f32(), self.read_f32())

    def read_texcoord(self) -> Tuple[float, float]:
        return (self.read_f32(), self.read_f32())

    def read_triangle(self) -> Tuple[int, int, int]:
        return (self.read_u16(), self.read_u16(), self.read_u16())


def parse_header(data: bytes) -> Header:
    reader = Reader(data)
    header_string, offset = _read_lf_string(data, 0)
    reader.offset = offset
    version = reader.read_u32()
    endian = reader.read_u8()
    user_version = reader.read_u32()
    num_blocks = reader.read_u32()
    num_block_types = reader.read_u16()
    block_types = [reader.read_string() for _ in range(num_block_types)]
    block_type_indices = [reader.read_u16() for _ in range(num_blocks)]
    unknown = reader.read_u32()
    return Header(
        header_string=header_string,
        version=version,
        endian=endian,
        user_version=user_version,
        num_blocks=num_blocks,
        num_block_types=num_block_types,
        block_types=block_types,
        block_type_indices=block_type_indices,
        unknown=unknown,
        offset=reader.offset,
    )


def parse_node_named(reader: Reader) -> Dict[str, Any]:
    return {"name": reader.read_string()}


def parse_node_general(reader: Reader) -> Dict[str, Any]:
    info = parse_node_named(reader)
    num_extra = reader.read_u32()
    info["extra_data_refs"] = [reader.read_i32() for _ in range(num_extra)]
    info["controller_ref"] = reader.read_i32()
    info["flags"] = reader.read_u16()
    return info


def parse_node_general_no_flag(reader: Reader) -> Dict[str, Any]:
    info = parse_node_named(reader)
    num_extra = reader.read_u32()
    info["extra_data_refs"] = [reader.read_i32() for _ in range(num_extra)]
    info["controller_ref"] = reader.read_i32()
    return info


def parse_node_geometry(reader: Reader) -> Dict[str, Any]:
    info = parse_node_general(reader)
    info["translation"] = reader.read_vector3()
    info["rotation"] = reader.read_matrix33()
    info["scale"] = reader.read_f32()
    num_props = reader.read_u32()
    info["properties"] = [reader.read_i32() for _ in range(num_props)]
    info["collision_ref"] = reader.read_i32()
    return info


def parse_node_controller(reader: Reader) -> Dict[str, Any]:
    info: Dict[str, Any] = {}
    info["next_controller"] = reader.read_u32()
    info["flags"] = reader.read_u16()
    info["frequency"] = reader.read_f32()
    info["phase"] = reader.read_f32()
    info["start_time"] = reader.read_f32()
    info["stop_time"] = reader.read_f32()
    info["target_ref"] = reader.read_i32()
    return info


def parse_node_tri_data(reader: Reader) -> Dict[str, Any]:
    info: Dict[str, Any] = {"group_id": reader.read_i32()}
    num_vertices = reader.read_u16()
    info["vertices_count"] = num_vertices
    info["keep_flags"] = reader.read_u8()
    info["compress_flags"] = reader.read_u8()
    has_vertices = reader.read_u8()
    if has_vertices:
        info["vertices"] = [reader.read_vector3() for _ in range(num_vertices)]
    data_flags = reader.read_u16()
    info["data_flags"] = data_flags
    has_normals = reader.read_u8()
    if has_normals:
        info["normals"] = [reader.read_vector3() for _ in range(num_vertices)]
    if data_flags & 0x1000:
        info["tangents"] = [reader.read_vector3() for _ in range(num_vertices)]
        info["bitangents"] = [reader.read_vector3() for _ in range(num_vertices)]
    info["bounding_center"] = reader.read_vector3()
    info["bounding_radius"] = reader.read_f32()
    has_colors = reader.read_u8()
    if has_colors:
        info["vertex_colors"] = [reader.read_color4() for _ in range(num_vertices)]
    uv_sets = data_flags % 4
    info["uv_sets"] = [
        [reader.read_texcoord() for _ in range(num_vertices)] for _ in range(uv_sets)
    ]
    info["consistency_flags"] = reader.read_u16()
    info["additional_data"] = reader.read_i32()
    info["triangles_count"] = reader.read_u16()
    return info


def parse_texture(reader: Reader) -> Dict[str, Any]:
    info = {
        "source": reader.read_u32(),
        "clamp_mode": reader.read_u32(),
        "filter_mode": reader.read_u32(),
        "uv_set": reader.read_u32(),
    }
    if reader.read_u8():
        info["translation"] = reader.read_texcoord()
        info["scale"] = reader.read_texcoord()
        info["rotation"] = reader.read_f32()
        info["transform_method"] = reader.read_u32()
        info["center"] = reader.read_texcoord()
    return info


def parse_shader(reader: Reader) -> Dict[str, Any]:
    info: Dict[str, Any] = {}
    if reader.read_u8():
        info["map_source"] = reader.read_u32()
        info["clamp_mode"] = reader.read_u32()
        info["filter_mode"] = reader.read_u32()
        info["uv_set"] = reader.read_u32()
        if reader.read_u8():
            info["translation"] = reader.read_texcoord()
            info["scale"] = reader.read_texcoord()
            info["rotation"] = reader.read_f32()
            info["transform_method"] = reader.read_u32()
            info["center"] = reader.read_texcoord()
        info["map_id"] = reader.read_u32()
    return info


def parse_match_group(reader: Reader) -> Dict[str, Any]:
    count = reader.read_u16()
    return {"indices": [reader.read_u16() for _ in range(count)]}


def parse_bone(reader: Reader, has_weights: bool) -> Dict[str, Any]:
    info = {
        "rotation": reader.read_matrix33(),
        "translation": reader.read_vector3(),
        "scale": reader.read_u32(),
        "sphere_center": reader.read_vector3(),
        "sphere_radius": reader.read_f32(),
    }
    if has_weights:
        num = reader.read_u16()
        info["weights"] = [
            (reader.read_u16(), reader.read_f32()) for _ in range(num)
        ]
    return info


def parse_bounding_volume(reader: Reader) -> Dict[str, Any]:
    kind = reader.read_u32()
    if kind == 0:
        return {"type": "sphere", "center": reader.read_vector3(), "radius": reader.read_f32()}
    if kind == 1:
        return {
            "type": "box",
            "center": reader.read_vector3(),
            "axis": [reader.read_vector3() for _ in range(3)],
            "extent": reader.read_vector3(),
        }
    if kind == 2:
        return {
            "type": "capsule",
            "center": reader.read_vector3(),
            "origin": reader.read_vector3(),
            "extent": reader.read_f32(),
            "radius": reader.read_f32(),
        }
    if kind == 4:
        num = reader.read_u32()
        return {
            "type": "union",
            "members": [parse_bounding_volume(reader) for _ in range(num)],
        }
    if kind == 5:
        return {
            "type": "halfspace",
            "plane": {
                "normal": reader.read_vector3(),
                "constant": reader.read_f32(),
            },
            "center": reader.read_vector3(),
        }
    if kind == 0xFFFFFFFF:
        return {"type": "default"}
    raise ValueError(f"Unknown bounding volume tag: {kind:#x}")


def parse_block(block_type: str, reader: Reader) -> Dict[str, Any]:
    if block_type == "NiNode":
        info = parse_node_geometry(reader)
        num_children = reader.read_u32()
        info["children"] = [reader.read_i32() for _ in range(num_children)]
        num_effects = reader.read_u32()
        info["effects"] = [reader.read_i32() for _ in range(num_effects)]
        return info
    if block_type == "NiCollisionData":
        info = {
            "target_ref": reader.read_i32(),
            "propagation_mode": reader.read_u32(),
            "collision_mode": reader.read_u32(),
        }
        if reader.read_u8():
            info["bounding_volume"] = parse_bounding_volume(reader)
        return info
    if block_type == "NiTriShape":
        info = parse_node_geometry(reader)
        info["data_ref"] = reader.read_i32()
        info["skin_instance_ref"] = reader.read_i32()
        has_shader = reader.read_u8()
        if has_shader:
            info["shader_name"] = reader.read_string()
            info["shader_extra_data"] = reader.read_i32()
        return info
    if block_type == "NiTexturingProperty":
        info = parse_node_general_no_flag(reader)
        info["apply_mode"] = reader.read_u32()
        texture_count = reader.read_u32()
        info["textures"] = []
        for index in range(texture_count):
            if reader.read_u8():
                tex = parse_texture(reader)
                if index == 5:
                    tex["bump_luma_scale"] = reader.read_f32()
                    tex["bump_luma_offset"] = reader.read_f32()
                    tex["bump_matrix"] = reader.read_matrix22()
                info["textures"].append(tex)
            else:
                info["textures"].append(None)
        shader_count = reader.read_u32()
        info["shader_textures"] = [parse_shader(reader) for _ in range(shader_count)]
        return info
    if block_type == "NiSourceTexture":
        info = parse_node_general_no_flag(reader)
        info["use_external"] = reader.read_u8()
        info["file_path"] = reader.read_string()
        info["pixel_data"] = reader.read_i32()
        info["pixel_layout"] = reader.read_u32()
        info["use_mipmaps"] = reader.read_u32()
        info["alpha_format"] = reader.read_u32()
        info["is_static"] = reader.read_u8()
        info["direct_render"] = reader.read_u8()
        return info
    if block_type == "NiAlphaProperty":
        info = parse_node_general(reader)
        info["threshold"] = reader.read_u8()
        return info
    if block_type == "NiMaterialProperty":
        info = parse_node_general_no_flag(reader)
        info["ambient_color"] = reader.read_color3()
        info["diffuse_color"] = reader.read_color3()
        info["specular_color"] = reader.read_color3()
        info["emissive_color"] = reader.read_color3()
        info["glossiness"] = reader.read_f32()
        info["alpha"] = reader.read_f32()
        return info
    if block_type == "NiAlphaController":
        info = parse_node_controller(reader)
        info["interpolator_ref"] = reader.read_i32()
        return info
    if block_type == "NiTriShapeData":
        info = parse_node_tri_data(reader)
        info["triangle_points_count"] = reader.read_u32()
        reader.read_u8()  # has_triangles
        info["triangles"] = [reader.read_triangle() for _ in range(info["triangles_count"])]
        match_groups = reader.read_u16()
        info["match_groups"] = [parse_match_group(reader) for _ in range(match_groups)]
        return info
    if block_type == "NiStencilProperty":
        info = parse_node_general_no_flag(reader)
        info["stencil_enabled"] = reader.read_u8()
        info["stencil_function"] = reader.read_u32()
        info["stencil_ref"] = reader.read_u32()
        info["stencil_mask"] = reader.read_u32()
        info["fail_action"] = reader.read_u32()
        info["zfail_action"] = reader.read_u32()
        info["pass_action"] = reader.read_u32()
        info["draw_mode"] = reader.read_u32()
        return info
    if block_type == "NiSkinInstance":
        return {
            "data_ref": reader.read_u32(),
            "skin_partition_ref": reader.read_u32(),
            "skeleton_root_ref": reader.read_u32(),
            "bones": [reader.read_u32() for _ in range(reader.read_u32())],
        }
    if block_type == "NiSkinData":
        info = {
            "skin_rotation": reader.read_matrix33(),
            "skin_translation": reader.read_vector3(),
            "skin_scale": reader.read_u32(),
            "bones_count": reader.read_u32(),
        }
        has_vertex_weights = reader.read_u8()
        info["bones"] = [
            parse_bone(reader, bool(has_vertex_weights)) for _ in range(info["bones_count"])
        ]
        return info
    raise NotImplementedError(f"Parser for {block_type} not implemented")


def parse_nif(path: Path) -> Dict[str, Any]:
    data = path.read_bytes()
    header = parse_header(data)
    reader = Reader(data, header.offset)
    blocks: List[Dict[str, Any]] = []
    for index, type_index in enumerate(header.block_type_indices):
        block_type = header.block_types[type_index]
        start_offset = reader.tell()
        try:
            block_data = parse_block(block_type, reader)
        except Exception as exc:
            raise RuntimeError(
                f"Failed to parse block {index} ({block_type}) at offset {start_offset:#x}"
            ) from exc
        blocks.append({"type": block_type, "data": block_data})
    return {"header": header, "blocks": blocks}


def extract_skeleton(nif: Dict[str, Any]) -> Dict[int, Dict[str, Any]]:
    nodes: Dict[int, Dict[str, Any]] = {}
    for idx, block in enumerate(nif["blocks"]):
        if block["type"] == "NiNode":
            nodes[idx] = block["data"]
    return nodes


def summarize_tree(nodes: Dict[int, Dict[str, Any]], root_index: int = 0) -> List[str]:
    lines: List[str] = []

    def _walk(idx: int, depth: int) -> None:
        info = nodes.get(idx)
        if not info:
            return
        name = info.get("name") or f"NiNode{idx}"
        lines.append(f'{"  " * depth}- [{idx}] {name}')
        for child in info.get("children", []):
            if child >= 0:
                _walk(child, depth + 1)

    _walk(root_index, 0)
    return lines


if __name__ == "__main__":
    import argparse

    TEXTURE_SLOT_NAMES = [
        "Base",
        "Dark",
        "Detail",
        "Gloss",
        "Glow",
        "Bump",
        "Decal0",
    ]

    def print_mesh_summary(nif: Dict[str, Any]) -> None:
        for idx, block in enumerate(nif["blocks"]):
            if block["type"] != "NiTriShape":
                continue
            tri_shape = block["data"]
            name = tri_shape.get("name") or f"NiTriShape{idx}"
            data_ref = tri_shape.get("data_ref", -1)
            skin_ref = tri_shape.get("skin_instance_ref", -1)
            if data_ref < 0 or data_ref >= len(nif["blocks"]):
                print(f"[{idx}] {name}: invalid data_ref {data_ref}")
                continue
            data_block = nif["blocks"][data_ref]
            if data_block["type"] != "NiTriShapeData":
                print(f"[{idx}] {name}: unexpected data block {data_block['type']}")
                continue
            mesh_data = data_block["data"]
            vertices = mesh_data.get("vertices") or []
            triangles = mesh_data.get("triangles") or []
            uv_sets = mesh_data.get("uv_sets") or []
            print(f"[{idx}] {name}")
            print(f"    Data block: {data_ref} ({len(vertices)} verts, {len(triangles)} faces, {len(uv_sets)} UV sets)")
            if mesh_data.get("normals"):
                print(f"    Normals: {len(mesh_data['normals'])}")
            if skin_ref >= 0:
                print(f"    Skin instance: block {skin_ref}")
            # Properties & materials
            prop_refs = tri_shape.get("properties") or []
            if prop_refs:
                print(f"    Properties:")
            for prop_idx in prop_refs:
                if prop_idx < 0 or prop_idx >= len(nif["blocks"]):
                    print(f"        [{prop_idx}] <invalid>")
                    continue
                prop_block = nif["blocks"][prop_idx]
                ptype = prop_block["type"]
                pdata = prop_block["data"]
                if ptype == "NiTexturingProperty":
                    print(f"        [{prop_idx}] {ptype}")
                    textures = pdata.get("textures") or []
                    for slot, tex in enumerate(textures):
                        if tex is None:
                            continue
                        slot_name = TEXTURE_SLOT_NAMES[slot] if slot < len(TEXTURE_SLOT_NAMES) else f"Slot{slot}"
                        source_ref = tex.get("source")
                        source_desc = ""
                        if isinstance(source_ref, int) and 0 <= source_ref < len(nif["blocks"]):
                            source_block = nif["blocks"][source_ref]
                            if source_block["type"] == "NiSourceTexture":
                                tex_data = source_block["data"]
                                source_desc = tex_data.get("file_path", "")
                        print(f"            {slot_name}: block {source_ref} {source_desc}")
                else:
                    print(f"        [{prop_idx}] {ptype}")

    parser = argparse.ArgumentParser(description="Parse Civ4 NIF files.")
    parser.add_argument("path", type=Path)
    parser.add_argument("--tree", action="store_true", help="Print NiNode hierarchy")
    parser.add_argument("--mesh-summary", action="store_true", help="Print NiTriShape/geometry details")
    args = parser.parse_args()

    nif = parse_nif(args.path)
    print(f"Parsed {args.path} ({nif['header'].num_blocks} blocks)")
    if args.tree:
        nodes = extract_skeleton(nif)
        for line in summarize_tree(nodes, 0):
            print(line)
    if args.mesh_summary:
        print_mesh_summary(nif)
