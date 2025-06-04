using System;
using System.Collections.Generic;

public class ComponentFactory
{
    public static Component CreateComponent(ComponentType type, MaterialType material, int Level = 1, ResourceType? bonusResource = null)
    {

        var myComponent = new Component(type, new List<ComponentTag>(), $"{material} {type}", Level);
        myComponent.addTag(GetComponentTagFromMaterial(material));
        if (bonusResource != null)
        {
            myComponent.addTag(GetComponentTagFromBonusResource(bonusResource?? throw new ArgumentNullException(nameof(bonusResource))));
        }
        myComponent.MaterialType = material;
        return myComponent;
    }

    private static ComponentTag GetComponentTagFromMaterial(MaterialType material)
    {
        return material switch
        {
            MaterialType.Iron    => ComponentTag.Heavy,
            MaterialType.Steel   => ComponentTag.Durable,
            MaterialType.Mythril => ComponentTag.Magical,
            MaterialType.Wood    => ComponentTag.Light,
            MaterialType.Rock    => ComponentTag.Blunt,
            _ => throw new ArgumentException("Invalid Material name.", nameof(material))
        };
    }
    
    private static ComponentTag GetComponentTagFromBonusResource(ResourceType resource)
    {
        return resource switch
        {
            ResourceType.Goblin_Heart      => ComponentTag.Brutal,
            ResourceType.Faery_Dust        => ComponentTag.Ethereal,
            ResourceType.Dragon_Scale      => ComponentTag.Draconian,
            ResourceType.Phoenix_Feather   => ComponentTag.Rebirth,
            ResourceType.Unicorn_Horn      => ComponentTag.Pure,
            ResourceType.Mermaid_Tear      => ComponentTag.Mystic,
            ResourceType.Elemental_Essence => ComponentTag.Elemental,
            ResourceType.Ancient_Rune      => ComponentTag.Cursed,
            _ => throw new ArgumentException("Invalid Resource name.", nameof(resource))
        };
    }
}
