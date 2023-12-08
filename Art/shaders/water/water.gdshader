shader_type spatial;

void vertex() {
	// Called for every vertex the material is visible on.
}

uniform vec3 albedo : source_color;
uniform float metallic : hint_range(0.0, 1.0) = 0;
uniform float roughness : hint_range(0.0, 1.0) = 0.02;
uniform sampler2D texture_normal;
uniform sampler2D texture_normal2;
uniform vec2 wave_direction = vec2(2.0,0.0);
uniform vec2 wave_2_direction = vec2(0.0,1.0);
uniform float time_scale : hint_range(0.0, 0.2, 0.005) = 0.025;

void fragment() {	
	vec3 normal = texture(texture_normal,UV).rgb;
	
	ALBEDO = albedo;
	METALLIC = metallic;
	ROUGHNESS = roughness;
	NORMAL_MAP = normal;
	
	// Time calculations for wave (normal map) movement
	vec2 time = (TIME * wave_direction) * time_scale;
	vec2 time2 = (TIME * wave_2_direction) * time_scale;
	// Blend normal maps into one
	vec3 normal_blend = mix(texture(texture_normal,UV + time).rgb, texture(texture_normal2,UV + time2).rgb, 0.5);
}

void light() {
	// Called for every pixel for every light affecting the material.
}
