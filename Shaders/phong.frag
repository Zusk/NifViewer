#version 330 core
out vec4 FragColor;

in vec3 vFragPos;
in vec3 vNormal;
in vec2 vUV;

uniform vec3 uLightPos;
uniform vec3 uViewPos;

uniform vec3 uAmbientColor;
uniform vec3 uDiffuseColor;
uniform vec3 uSpecularColor;
uniform float uShininess;

uniform sampler2D uTexture;

void main()
{
    vec3 N = normalize(vNormal);
    vec3 L = normalize(uLightPos - vFragPos);

    float diff = max(dot(N,L),0.0);
    vec3 diffuse = diff * uDiffuseColor;

    vec3 ambient = uAmbientColor;

    vec3 V = normalize(uViewPos - vFragPos);
    vec3 R = reflect(-L, N);
    float spec = pow(max(dot(V,R),0.0), uShininess);
    vec3 specular = spec * uSpecularColor;

    vec4 texColor = texture(uTexture, vUV);

    vec3 final = (ambient + diffuse + specular) * texColor.rgb;

    FragColor = vec4(final, texColor.a);
}
