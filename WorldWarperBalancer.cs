using log4net;
using log4net.Repository.Hierarchy;
using MonoMod.RuntimeDetour.HookGen;
using System;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WorldWarperPort.Items;
using WorldWarperPort.WWPlayer;

namespace WorldWarperBalancer {
    public class WorldWarperBalancer : ModSystem {
        public override void AddRecipes() {
            base.AddRecipes();
            int worldWarper = ModContent.ItemType<WorldWarper>();
            Recipe recipe = Recipe.Create(worldWarper);
            recipe.AddIngredient(ItemID.ShadowScale, 10)
                .AddIngredient(ItemID.DemoniteBar, 50)
                .AddIngredient(ItemID.EoCShield, 1)
                .AddRecipeGroup("baseEmblems")
                .AddTile(TileID.WorkBenches)
                .Register();


            recipe = Recipe.Create(worldWarper);
            recipe.AddIngredient(ItemID.TissueSample, 10)
                .AddIngredient(ItemID.CrimtaneBar, 50)
                .AddIngredient(ItemID.EoCShield, 1)
                .AddRecipeGroup("baseEmblems")
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void PostAddRecipes() {
            base.PostAddRecipes();
            foreach (var recipe in Main.recipe)
                if (recipe.HasResult(ModContent.ItemType<WorldWarper>()) && !recipe.HasRecipeGroup("baseEmblems"))
                    recipe.DisableRecipe();
        }

        public override void AddRecipeGroups() {
            base.AddRecipeGroups();
            RecipeGroup group = new(() => "Any base emblem",
                new int[] { ItemID.SorcererEmblem, ItemID.SummonerEmblem, ItemID.WarriorEmblem, ItemID.RangerEmblem });
            RecipeGroup.RegisterGroup("baseEmblems", group);
        }
    }

    class WorldWarperPlayerFix : ModPlayer {
        public override void ResetEffects() {
            base.ResetEffects();
            Player.GetModPlayer<WorldWarperPlayer>().WorldWarperPresent = false;
        }
    }
}
