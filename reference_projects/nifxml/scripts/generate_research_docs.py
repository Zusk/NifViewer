#!/usr/bin/env python3
"""
Generate a research-style documentation tree for each block defined in nif.xml.
Each top-level block becomes a Markdown file so the dataset is easy to consume
in smaller fragments.
"""

import os
import re
import shutil
import textwrap
import xml.etree.ElementTree as ET

BASE_DIR = "research"
TAG_ORDER = [
    "token",
    "verattr",
    "version",
    "module",
    "basic",
    "bitfield",
    "bitflags",
    "enum",
    "struct",
    "niobject",
]
TAG_DIR_MAP = {
    "bitflags": "bitflags",
}


def sanitize_text(text: str) -> str:
    if not text:
        return ""
    result = textwrap.dedent(text).strip()
    return result


def slugify(name: str) -> str:
    slug = re.sub(r"[^0-9A-Za-z]+", "-", name.strip())
    slug = slug.strip("-")
    return slug or "unnamed"


def format_attr_list(attrs, exclude=None):
    exclude = exclude or set()
    parts = []
    for attr in sorted(attrs):
        if attr in exclude:
            continue
        value = attrs[attr]
        parts.append(f"`{attr}`=`{value}`")
    return ", ".join(parts)


def render_field(field):
    title = field.get("name", "Unnamed Field")
    type_name = field.get("type")
    line = f"- **{title}**"
    if type_name:
        line += f" (`{type_name}`)"
    extras = format_attr_list(field.attrib, exclude={"name", "type"})
    sub_bullets = []
    if extras:
        sub_bullets.append(f"Attributes: {extras}")
    desc = sanitize_text(field.text)
    if desc:
        sub_bullets.append(desc)
    if sub_bullets:
        line += "\n" + "\n".join(f"  - {sub}" for sub in sub_bullets)
    return line


def write_fields_section(output_file, fields):
    if not fields:
        return
    output_file.write("## Fields\n")
    for field in fields:
        output_file.write(render_field(field) + "\n")
    output_file.write("\n")


def render_child_line(child):
    primary = child.get("name") or child.get("token") or child.get("id")
    line = f"- `{child.tag}`"
    if primary:
        line += f" `{primary}`"
    attrs = format_attr_list(child.attrib, exclude={"name", "token", "id"})
    if attrs:
        line += f" ({attrs})"
    desc = sanitize_text(child.text)
    if desc:
        line += f" – {desc}"
    return line


def write_child_section(output_file, title, children):
    if not children:
        return
    output_file.write(f"## {title}\n")
    for child in children:
        output_file.write(render_child_line(child) + "\n")
    output_file.write("\n")


def write_documentation(elements_by_tag):
    if os.path.isdir(BASE_DIR):
        shutil.rmtree(BASE_DIR)
    os.makedirs(BASE_DIR, exist_ok=True)

    counts = {}

    for tag in TAG_ORDER:
        elems = elements_by_tag.get(tag, [])
        dir_name = TAG_DIR_MAP.get(tag, f"{tag}s")
        dir_path = os.path.join(BASE_DIR, dir_name)
        os.makedirs(dir_path, exist_ok=True)
        counts[tag] = len(elems)
        for index, elem in enumerate(elems, start=1):
            name = elem.get("name") or elem.get("id") or f"{tag}-{index}"
            filename = slugify(name)
            path = os.path.join(dir_path, f"{filename}.md")
            with open(path, "w", encoding="utf-8") as out:
                out.write(f"# {tag.title()} `{name}`\n\n")
                desc = sanitize_text(elem.text)
                if desc:
                    out.write(f"{desc}\n\n")
                if elem.attrib:
                    out.write("## Attributes\n")
                    for attr, val in sorted(elem.attrib.items()):
                        out.write(f"- **{attr}**: `{val}`\n")
                    out.write("\n")

                if tag in {"struct", "niobject"}:
                    fields = [child for child in elem if child.tag == "field"]
                    write_fields_section(out, fields)
                    others = [child for child in elem if child.tag != "field"]
                    if others:
                        write_child_section(out, "Supplementary entries", others)
                elif tag == "token":
                    write_child_section(out, "Entries", list(elem))
                elif tag in {"enum", "bitfield", "bitflags"}:
                    write_child_section(out, "Values", list(elem))
                else:
                    remaining = list(elem)
                    if remaining:
                        write_child_section(out, "Child entries", remaining)

    readme_path = os.path.join(BASE_DIR, "README.md")
    with open(readme_path, "w", encoding="utf-8") as readme:
        readme.write("# NIF XML Research Tree\n\n")
        readme.write(
            "Every top-level block from `nif.xml` is captured in its own Markdown "
            "file below so that agents can reason about pieces of the format in "
            "isolation.\n\n"
        )
        readme.write("## Sections\n\n")
        for tag in TAG_ORDER:
            count = counts.get(tag, 0)
        dir_name = TAG_DIR_MAP.get(tag, f"{tag}s")
        readme.write(
            f"- [`{dir_name}/`](./{dir_name}/) – {count} {tag} definitions.\n"
        )
        readme.write(
            "\nRun `python3 scripts/generate_research_docs.py` from the repository "
            "root to regenerate this tree after updating `nif.xml`.\n"
        )


def main():
    tree = ET.parse("nif.xml")
    root = tree.getroot()
    elements_by_tag = {tag: [] for tag in TAG_ORDER}
    for child in root:
        if child.tag in elements_by_tag:
            elements_by_tag[child.tag].append(child)
    write_documentation(elements_by_tag)


if __name__ == "__main__":
    main()
