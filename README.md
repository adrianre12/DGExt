# DGExt
These are extensions for Devion Games Inventory System that I am using in my games that I think could be useful to others. The current features are;
- Sub-ActionTemplates
  - Conditional flow for Trigger behaviours.
  - Using Action Templates in Trigger behaviours.
  - Using more than one Action Template.
  - Trigger prompts can be changed by a behaviour Action or custom scripts.
  - Outlining of items when the Trigger prompt is displayed.
  - Being able to use the Quest window from within an action.
- Examine
  - Adds to the item trigger the option to examine an item. 
  - Rotate, Zoom and Pan the item.
  - Optional blurring of scene while examining an item. 

The project includes the scenes;
- Demo: Shows the Sub-ActionTemplate use cases. More details can be found in [Sub-ActionTemplates.pdf](/Documentation/Sub-ActionTemplates.pdf)
- Examine: Is an example of how to configure an object to be examined. More details can be found in [Examine.pdf](/Documentation/Examine.pdf)

Videos can be found in the playlist [DGExt](https://www.youtube.com/playlist?list=PLCZolJ4GDO6_87a42ku9bACn9FbKyGVQJ)

## Install
This requires the Free asset 'Devion Games Inventory Manager' or the Paid for 'Devion Games RPG Kit', and the Unity PostProcessing package.
Note: The PostProcessing package is only required if DGExtExamples or the Examine function are being used.
Note: Optionally it is possible to add item outlining with the free asset 'Quick Outline'.

Steps to install the examples;
- Create a new Unity Project.
- Using the Package Manager install the PostProcessing package.
- Using the Package Manager install the Devion Games Asset.
- If using the free version 'Inventory Manager'
  - Use the Devion Games Module Manager to install the Quest System and Quest Examples.
  - Use the Inventory systems Integration asset under Quest System Integrations.
- Download and install the DGExt asset.
- Download and install the DGExtExamples asset.
- Follow the section 'Setting up the Examine scene' in [Examine.pdf](/Documentation/Examine.pdf)

The example scenes are under Assets > DGExtExamples > Scenes.

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