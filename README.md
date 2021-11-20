# DGExt
These are extensions for Devion Games Inventory System that I am using in my games that I think could be useful to others. The current features are;
- Conditional flow for Trigger behaviours.
- Using Action Templates in Trigger behaviours.
- Using more than one Action Template.
- Trigger prompts can be changed by a behaviour Action or custom scripts.
- Outlining of items when the Trigger prompt is displayed.
- Being able to use the Quest window from within an action.

The project includes a Demo scene to show use cases. More details can be found in the [documentation.](/Documentation/Sub-ActionTemplates.pdf)

## Install
This requires the Free assets 'Devion Games Inventory Manager' or the Paid for 'RPG Kit'
Optionally it is possible to add item outlining the free asset 'Quick Outline'.

Steps to run the demo;
- Create a new Unity Project.
- Start Unity and add the Devion Games Asset.
- Using the Devion Module Manager install the Quest System and Quest Examples.
- Use the Inventory systems Integration asset under Quest System Integrations.
- Download and install the DGExt asset.
- Download and install the DGExtExamples asset.
- Under Assets > SGExtExamples > Scenes open and play the Demo scene.

## Adding Outlining
An example of how to do item outlining can be added. This will outline items with a red border when the tooltip is displayed.
This is done using the free asset 'QuickOutline' the steps to use it are;
- Using the Package Manager install QuickOutline.
- Fix Quick Outline using the section below.
- Add QuickOutline to the DGExt.asmDef file
- Edit DisplayTriggerBlackboardToolTip.cs and uncomment lines 22-35 and line 54.

## Fixing QuickOutline
As delivered the asset has a class name clash with UnityEngine.UI.Outline, to fix this the simplest solution is to edit Outline.cs and set the namespace QuickOutline.

```
namespace QuickOutline
{
    [DisallowMultipleComponent]
    public class Outline : MonoBehaviour

...
}
```

Then in the QuickOutline folder create an Assembly Definition file called QuickOutline, it just has to exist, it does not need to be edited.