using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateForge.Managers
{
    /// <summary>
    /// Contains all the string values for various combo boxes.
    /// </summary>
    public static class ComboBoxValues
    {
        private static Dictionary<string, List<string>> _comboBoxDictionary = new Dictionary<string, List<string>>()
        {
            { "Block Type", new List<string>()
                {
                    "air","stone","grass","dirt","cobblestone","planks","sapling","bedrock","flowing_water","water",
                    "flowing_lava","lava","sand","gravel","gold_ore","iron_ore","coal_ore","log","leaves","sponge","glass",
                    "lapis_ore","lapis_block","dispenser","sandstone","noteblock","bed","golden_rail","detector_rail",
                    "sticky_piston","web","tallgrass","deadbush","piston","piston_head","wool","yellow_flower","red_flower",
                    "brown_mushroom","red_mushroom","gold_block","iron_block","double_stone_slab","stone_slab","brick_block",
                    "tnt","bookshelf","mossy_cobblestone","obsidian","torch","fire","mob_spawner","oak_stairs","chest",
                    "redstone_wire","diamond_ore","diamond_block","crafting_table","wheat","farmland","furnace","lit_furnace",
                    "standing_sign","wooden_door","ladder","rail","stone_stairs","wall_sign","lever","stone_pressure_plate",
                    "iron_door","wooden_pressure_plate","redstone_ore","lit_redstone_ore","unlit_redstone_torch","redstone_torch",
                    "stone_button","snow_layer","ice","snow","cactus","clay","reeds","jukebox","fence","pumpkin","netherrack",
                    "soul_sand","glowstone","portal","lit_pumpkin","cake","unpowered_repeater","powered_repeater","stained_glass",
                    "trapdoor","monster_egg","stonebrick","brown_mushroom_block","red_mushroom_block","iron_bars","glass_pane",
                    "melon_block","vine","fence_gate","brick_stairs","stone_brick_stairs","mycelium","waterlily","nether_brick",
                    "nether_brick_fence","nether_brick_stairs","nether_wart","enchanting_table","brewing_stand","cauldron","end_portal",
                    "end_portal_frame","end_stone","dragon_egg","redstone_lamp","lit_redstone_lamp","double_wooden_slab","wooden_slab",
                    "cocoa","sandstone_stairs","emerald_ore","ender_chest","tripwire_hook","emerald_block","spruce_stairs","birch_stairs",
                    "jungle_stairs","command_block","beacon","cobblestone_wall","flower_pot","carrots","potatoes","wooden_button",
                    "skull","anvil","trapped_chest","light_weighted_pressure_plate","heavy_weighted_pressure_plate","unpowered_comparator",
                    "powered_comparator","daylight_detector","redstone_block","quartz_ore","hopper","quartz_block","quartz_stairs",
                    "activator_rail","dropper","stained_hardened_clay","stained_glass_pane","leaves2","log2","acacia_stairs",
                    "dark_oak_stairs","slime","barrier","iron_trapdoor","prismarine","sea_lantern","hay_block","carpet","hardened_clay",
                    "coal_block","packed_ice","double_plant","standing_banner","wall_banner","daylight_detector_inverted",
                    "red_sandstone","red_sandstone_stairs","double_stone_slab2","stone_slab2","spruce_fence_gate","birch_fence_gate",
                    "jungle_fence_gate","dark_oak_fence_gate","acacia_fence_gate","spruce_fence","birch_fence","jungle_fence",
                    "dark_oak_fence","acacia_fence","spruce_door","birch_door","jungle_door","acacia_door","dark_oak_door","end_rod",
                    "chorus_plant","chorus_flower","purpur_block","purpur_pillar","purpur_stairs","purpur_double_slab","purpur_slab",
                    "end_bricks","beetroots","grass_path","end_gateway","repeating_command_block","chain_command_block","frosted_ice",
                    "magma","nether_wart_block","red_nether_brick","bone_block","structure_void","observer","white_shulker_box",
                    "orange_shulker_box","magenta_shulker_box","light_blue_shulker_box","yellow_shulker_box","pink_shulker_box",
                    "gray_shulker_box","silver_shulker_box","cyan_shulker_box","purple_shulker_box","blue_shulker_box",
                    "brown_shulker_box","green_shulker_box","red_shulker_box","black_shulker_box","concrete","concrete_powder",
                    "structure_block"
                } },
            { "Item Type", new List<string>()
                {
                    "acacia_door","acacia_boat","apple","armor_stand","arrow","baked_potato","banner","bed","beef","beetroot","beetroot_seeds",
                    "beetroot_soup","birch_boat","birch_door","boat","bone","book","bow","bowl","bread","brewing_stand","brick","blaze_powder",
                    "blaze_rod","bucket","cake","carrot","carrot_on_a_stick","cauldron","charcoal","chainmail_boots","chainmail_chestplate",
                    "chainmail_helmet","chainmail_leggings","chest_minecart","chicken","chorus_fruit","clay","clock","coal","command_block_minecart",
                    "comparator","cooked_beef","cooked_chicken","cooked_fish","cooked_mutton","cooked_rabbit","cookie","compass","dark_oak_door",
                    "dark_oak_boat","diamond","diamond_axe","diamond_boots","diamond_chestplate","diamond_helmet","diamond_hoe","diamond_horse_armor",
                    "diamond_leggings","diamond_pickaxe","diamond_shovel","diamond_sword","dragon_breath","dye","egg","elytra","enchanted_book",
                    "end_crystal","ender_eye","ender_pearl","emerald","experience_bottle","feather","fermented_spider_eye","filled_map",
                    "firework_charge","fireworks","fish","fishing_rod","fire_charge","flint","flint_and_steel","flower_pot","furnace_minecart",
                    "ghast_tear","glass_bottle","glowstone_dust","gold_ingot","gold_nugget","golden_apple","golden_axe","golden_boots","golden_carrot",
                    "golden_chestplate","golden_helmet","golden_hoe","golden_horse_armor","golden_leggings","golden_pickaxe","golden_shovel",
                    "golden_sword","gunpowder","hopper_minecart","iron_axe","iron_boots","iron_chestplate","iron_door","iron_helmet","iron_hoe",
                    "iron_horse_armor","iron_ingot","iron_leggings","iron_nugget","iron_pickaxe","iron_shovel","iron_sword","item_frame",
                    "jungle_boat","jungle_door","knowledge_book","lava_bucket","lead","leather","leather_boots","leather_chestplate","leather_helmet",
                    "leather_leggings","lingering_potion","magma_cream","map","melon","melon_seeds","milk_bucket","minecart","mutton","mushroom_stew",
                    "name_tag","netherbrick","nether_star","nether_wart","paper","painting","poisonous_potato","popped_chorus_fruit","potato",
                    "potion","porkchop","prismarine_crystals","prismarine_shard","pumpkin_pie","pumpkin_seeds","quartz","rabbit","rabbit_foot",
                    "rabbit_hide","rabbit_stew","record_11","record_13","record_blocks","record_cat","record_chirp","record_far","record_mall",
                    "record_mellohi","record_stal","record_strad","record_wait","record_ward","redstone","reeds","repeater","rotten_flesh",
                    "saddle","shears","shield","shulker_shell","sign","skull","slimeball","snowball","spawn_egg","speckled_melon","spider_eye",
                    "splash_potion","spruce_boat","spruce_door","stick","stone_axe","stone_hoe","stone_pickaxe","stone_shovel","stone_sword",
                    "string","sugar","totem_of_undying","tnt_minecart","water_bucket","wheat","wheat_seeds","wooden_axe","wooden_door",
                    "wooden_hoe","wooden_pickaxe","wooden_shovel","wooden_sword","writable_book","written_book"
                } },
            { "Potion Type", new List<string>()
                {
                    "absorption","bad_luck","blindness","damage_resistance","fire_resistance","glowing","haste","health_boost","hunger",
                    "instant_health","instant_damage","invisibility","jump_boost","levitation","luck","mining_fatigue","naseua",
                    "night_vision","poison","regeneration","saturation","slowness","speed","strength","water_breathing","weakness","wither"
                } },
            { "Achievement Type", new List<string>()
                {
                    "acquire_iron","bake_cake","bookcase","breed_cow","brew_potion","build_better_pickaxe","build_furnace","build_hoe",
                    "build_pickaxe","build_sword","build_workbench","cook_fish","diamonds_to_you","enchantments","end_portal","explore_all_biomes",
                    "fly_pig","full_beacon","get_blaze_rod","get_diamonds","ghast_return","kill_cow","kill_enemy","kill_wither","make_break",
                    "mine_wood","nether_portal","on_a_rail","open_inventory","overkill","overpowered","snipe_skeleton","spawn_wither","the_end",
                } },
            { "Weather Type", new List<string>()
                {
                    "rain","storm","sun"
                } },
            { "Enchantment Type", new List<string>()
                {
                    "arrow_damage","arrow_fire","arrow_infinite","arrow_knockback","binding_curse","damage_all","damage_arthropods","damage_undead",
                    "depth_strider","dig_speed","durability","fire_aspect","knockback","loot_bonus_blocks","loot_bonus_mobs","luck","lure",
                    "mending","oxygen","protection_environmental","protection_explosions","protection_fall","protection_fire","protection_projectile",
                    "silk_touch","sweeping_edge","thorns","vanishing_curse","water_worker"
                } },
            { "Mob Type", new List<string>()
                {
                    "area_effect_cloud","armor_stand","arrow","bat","blaze","boat","cave_spider","chicken","complex_part",
                    "cow","creeper","donkey","dragon_fireball","dropped_item","egg","elder_guardian","ender_crystal",
                    "ender_dragon","ender_pearl","ender_signal","enderman","endermite","evoker","evoker_fangs","experience_orbs",
                    "falling_block","fireball","firework","fishing_hook","ghast","giant","guardian","horse","husk","illusioner",
                    "iron_golem","item_frame","leash_hitch","lightning","lingering_potion","llama","llama_spit","magma_cube",
                    "minecart","minecart_chest","minecart_command","minecart_furnace","minecart_hopper","minecart_mob_spawner",
                    "minecart_tnt","mule","mushroom_cow","ocelot","painting","parrot","pig","pig_zombie","player","polar_bear",
                    "primed_tnt","rabbit","sheep","shulker","shulker_bullet","silverfish","skeleton","skeleton_horse","slime",
                    "small_fireball","snowball","snowman","spectral_arrow","spider","splash_potion","squid","stray","thrown_exp_bottle",
                    "tipped_arrow","unknown","vex","villager","vindicator","weather","witch","wither","wither_skeleton","wither_skull",
                    "wolf","zombie","zombie_horse","zombie_villager"
            } },
            { "Mob_Tameable Type", new List<string>()
                {
                    "horse","ocelot","wolf"
            } },
            { "YN_Answer Type", new List<string>()
                {
                    "yes","no"
            } },
            { "Journal_Answer Type", new List<string>()
                {
                    "add","del","update"
            } },
            { "AddDel_Answer Type", new List<string>()
                {
                    "add","del"
            } },
            { "Lever_Answer Type", new List<string>()
                {
                    "on","off","toggle"
            } }

        };

        public static Dictionary<string, List<string>> ComboBoxDictionary { get => _comboBoxDictionary; }
    }
}
