# DGExt
Extensions for Devion Games Inventory System. The features added are;
- Conditional flow for Trigger behaviours.
- Using Action Templates in Trigger behaviours.
- Using more than one Action Template.
- Trigger prompts can be changed by  a behaviour Action or custom scripts.
- Outlining of items when the Trigger prompt is displayed.

The project includes a Demo scene to show use cases of the functionality.

## Install
This requires the Free assets 'Devion Games Inventory Manager' and 'Quick Outline' all though it is easy to remove the requirement for 'Quick Outline' if not required.
Steps to run the demo;
- Clone this repository.
- Start Unity and add the required assets.
- Fix a class naming issue in 'Quick Outline'. See bellow.

## Quick Outline
If outlining is not required edit DisplayTriggerBlackboardToolTip.cs and comment out lines 22-35 and line 54.

As delivered the asset has a class name clash with UnityEngine.UI.Outline, to fix this the simplest solution is to edit Outline.cs and set the namespace QuickOutline.

```
namespace QuickOutline
{
    [DisallowMultipleComponent]
    public class Outline : MonoBehaviour

...
}
```

Then in the Quick Outline folder create an Assembly Definition file called QuickOutline, it just has to exist, it does not need to be edited.