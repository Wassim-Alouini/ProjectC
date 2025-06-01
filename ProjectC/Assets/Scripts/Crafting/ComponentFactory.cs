using System;
using System.Collections.Generic;

public class ComponentFactory
{
    public static Component CreateComponent(ComponentType type, MaterialItem material, ResourceItem bonusResource = null)
    {

        var myComponent = new Component(type, new List<ComponentTag>(), $"{material.Name} {type}");
        myComponent.addTag(GetComponentTagFromMaterial(material));
        if (bonusResource != null)
        {
            myComponent.addTag(GetComponentTagFromBonusResource(bonusResource));
        }
        return myComponent;
    }

    private static ComponentTag GetComponentTagFromMaterial(MaterialItem material)
    {
        return material.MaterialType switch
        {
            MaterialType.Iron    => ComponentTag.Heavy,
            MaterialType.Steel   => ComponentTag.Durable,
            MaterialType.Mythril => ComponentTag.Magical,
            MaterialType.Wood    => ComponentTag.Light,
            MaterialType.Rock    => ComponentTag.Blunt,
            _ => throw new ArgumentException("Invalid Material name.", nameof(material))
        };
    }
    
    private static ComponentTag GetComponentTagFromBonusResource(ResourceItem resource)
    {
        return resource.ResourceType switch
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
