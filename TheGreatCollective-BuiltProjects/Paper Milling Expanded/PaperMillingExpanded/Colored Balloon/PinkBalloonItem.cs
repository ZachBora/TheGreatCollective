﻿using System;
using System.Collections.Generic;
using Eco.Core.Items;
using Eco.Gameplay.Housing;
using Eco.Gameplay.Items;
using Eco.Gameplay.Systems.Tooltip;
using Eco.Gameplay.Housing.PropertyValues;
using Eco.Gameplay.Components;
using Eco.Gameplay.Skills;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.Shared.Math;

namespace Eco.Mods.TechTree
{
    [Serialized]
    [LocDisplayName("Pink Balloon")]
    [Ecopedia("Housing Objects", "Decoration", true, display: InPageTooltip.DynamicTooltip)]
    [Tag("Housing", 1)]
    [Tag("Balloon", 1)]
    public class PinkBalloonItem : WorldObjectItem<PinkBalloonObject>
    {
        public override LocString DisplayDescription => Localizer.DoStr("For celebrating.");
        public override DirectionAxisFlags RequiresSurfaceOnSides { get; } = 0 | DirectionAxisFlags.Down;
        [TooltipChildren]
        public HomeFurnishingValue HousingTooltip => HousingVal;

        [TooltipChildren]
        public static HomeFurnishingValue HousingVal => new()
        {
            Category = 0,
            SkillValue = 0.2f,
            TypeForRoomLimit = Localizer.DoStr("Decoration"),
            DiminishingReturnPercent = 0.1f
        };
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(HousingComponent))]
    [RequireComponent(typeof(SolidAttachedSurfaceRequirementComponent))]
    public class PinkBalloonObject : WorldObject, IRepresentsItem
    {
        public override LocString DisplayName => Localizer.DoStr("Pink Balloon");

        public override TableTextureMode TableTexture => TableTextureMode.Paper;

        public virtual Type RepresentedItemType => typeof(PinkBalloonItem);

        protected override void Initialize()
        {
            base.GetComponent<HousingComponent>(null).HomeValue = PinkBalloonItem.HousingVal;
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [RequiresSkill(typeof(PaperMillingSkill), 3)]
    public class PinkBalloonRecipe : Recipe
    {
        public PinkBalloonRecipe()
        {
            base.Name = "PinkBalloon";
            base.DisplayName = Localizer.DoStr("Pink Balloon");
            base.Ingredients = new List<IngredientElement>
            {
                new IngredientElement(typeof(ColouredPaperPinkItem), 4f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent)),
                new IngredientElement(typeof(PlantFibersItem), 10f, typeof(PaperMillingSkill), typeof(PaperMillingLavishResourcesTalent))
            };
            base.Items = new List<CraftingElement>
            {
                new CraftingElement<PinkBalloonItem>(1f)
            };
            CraftingComponent.AddTagProduct(typeof(PaperMillingWorkBenchObject), typeof(BlueBalloonRecipe), this);
        }
    }
}
