
/* Lead Dev: Noah Michael Lane Fuentes
 * Studio Name: TBD
 * Project Name: TBD
 * 
 * 
 * This doc is for keeping ideas and notes about the gameplay loop and mechanics. Basically a word
 * doc but this was faster and keeps the project together.
 * 
 * 
 * Game play is a survival based kingdom builder and boss fighter.
 * 
 * Survival:
 *      Health:
 *          Damage taken from enemies and environment like lava/fire stuff. Armor can reduce damage
 *          from enemies. Types of dmg; true, physical, fire, ice, and poison.
 * 
 *      Food:
 *          Need to keep food supply to keep kingdom from starving. (Read the resource section)
 *          ???What effect does a food shortage have on the kingdom and player???
 *          ???What effect does a food outage have on the kingdom and player???
 *      
 *      Water:
 *          Need to keep a water supply to keep kingdom from dehydrating. (Read the resource section)
 *          ???What effect does a water shortage have on the kingdom and player???
 *          ???What effect does a water outage have on the kingdom and player???
 * 
 * 
 * Resources:
 *      Food:
 *          Player can supply and prepare food for kingdom. As player progresses, will gain NPCs with 
 *          food supply occupation and food prep occupation to automate this process.
 *          If the kingdom food bar falls low (whatever low means), a hunger
 *          effect falls over the kingdom. Outages of food have a worse effect.
 *      
 *      Water:
 *          Player can supply water for kingdom. As player progresses, will gain NPCs with 
 *          water supply occupation to automate this process. If the kingdom water bar 
 *          falls low (whatever low means), a thirt effect falls over the kingdom. 
 *          Outages of water have a worse effect.
 *          
 *      Base Materials:
 *          The kingdom has a communal pool for each resource. 
 *          (Currently: Food, water, wood, textiles, stone, iron, gold, crystal, and black crystal) 
 * 
 * Explore:
 *      POIs will provide components (like Rust) that are used in crafting along with items that
 *      help the kingdom sustain and grow. These areas have higher density of enemies and a chance
 *      at spawning an NPC (If the NPC's spawn criteria are met... Like Terraria)
 * 
 * Craft:
 *      Use kingdom resources and found components at an NPC to craft items. (Some NPCs will 
 *      be crafters of specific items)
 * 
 * Kingdom Building/Managment:
 *      Use kingdom resources to build buildings to facilitate NPC work. Housing will be needed
 *      to increase population cap. Walls can be built for defense. Storage facilities will increase
 *      resource caps. Can upgrade buildings to upgrade cooresponding NPCs.
 * 
 * NPC interaction:
 *      NPCs will come with predetermined occupation. After certain milestones are reached through 
 *      normal progression (boss kill, biome explored, resource gathered... etc) POIs will have a
 *      chance to spawn NPC for player to save. NPC will join the kingdom the next night if there is
 *      available housing. They can do their job once the kingdom has an open facility pertaining to
 *      their occupation.
 *      
 *      Occupations:
 *          Resource Management: (Can have multiple of each of these)
 *              Water Bearer    (gathers water from a well, adds to kingdom supply)
 *              Farmer          (gathers raw food and fabric from farm building the kingdom)
 *              Cook            (turns raw food to edible food in a kitchen, adds to kingdom supply)
 *              Lumberjack      (gathers wood near a lumber mill from the wilderness, adds to the kingdom supply)
 *              Miner           (gathers stone, raw iron, raw gold, and crystal from the wilderness near a mine, adds to kingdom supply)
 *              
 *          Crafters: (Only one can be alive at a time)
 *              Artisan         (Player interacts to craft general items at a workshop)
 *              BlackSmith      (Turns raw metals into ingots. Player interacts to craft metalworks at a armory)
 *              Builder         (Player interacts to build advanced buildings at a planning quarters)
 *              Mage            (Player interacts to craft magic items at a mage tower)
 * 
 *          General:
 *              Soldier         (Do I implement this? Give them a weapon and armor, can take some out, they defend kingdom)
 * 
 * 
 * Bosses:
 *      Mini-Bosses: (Kill for materials and specialized components)
 *      
 *      Bosses: (Kill to progress game and unlock new NPCs)
 * 
 * 
 * 
 * 
 * 
 * 
 *  Things to think about:
 *  
 *      Should the player be able to pick where the kingdom/campsite starts? part of map generation could decide this for them.
 *          I think the map should not be procedural for now, and the camp starts in a prime location
 *      
 * 
 * 
 * 
 * 
 * 
 */
