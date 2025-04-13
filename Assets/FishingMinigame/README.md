<div align="center">
  
# Fishing Minigame

</div>

## About the minigame

It's a mini game based on fishing from stardew valley. recreated from scratch using my own code and assets. 
I obtained all data from the official wiki and analysis clips from game. 
<div align="center">
  <div>
  <img alt="menu scene" src="https://github.com/user-attachments/assets/5b0ea23d-ff9c-4b28-9a14-fdb902043810" height = "350px">
  <img alt="Fishing scene" src="https://github.com/user-attachments/assets/45586d8a-6636-4b45-a603-5e33896b84c8" height = "350px">
    </div>
</div>

## About the minigame from the creation side

### Research
I spent a lot of time researching and choosing the right values ​​so that players can feel at least a little of the same as in the original. But trying to recreate fish behavior was be the biggest challenge. 
What confused me the most was the information about the floater and sinker fishes on the main wiki, so I spent hours trying to recreate their behavior by speeding up their movement in one of the directions. 
But i notice their behavior, depending on their type - the floater almost always end on very top when he started moving up, and the sinker almost always end at the very bottom after he started moving down. Only this behaviour allowed me to come up with a solution and it was simpler than i thought. 
Just moved them consistently in the right direction, up or down depends on fish type.


### Scripts

As for the code I have:
 - two scripts responsible for the UI, one for setting menu and another one for minigame ui
([MenuUI.cs](Scripts/MenuUi.cs)
and [FishingUI.cs](Scripts/FishingUI.cs))
 - the main script responsible for the minigame ([FishingController.cs](Scripts/FishingController.cs))
 - various fish behaviors ([FishBehaviour.cs](Scripts/FishBehaviour.cs))
 - fishing bar behaviour ([FishingBarBehaviour.cs](Scripts/FishingBarBehaviour.cs))


The behavior of the fish did not differ much from each other, so inheritance seems to have worked very well in this case. 
However, the application of these behaviors is perfectly described by the strategy pattern. Example below.
```C#
public interface IFish
{
  public void Move(ref float fishPosition);
}

public class FishingController : MonoBehaviour
{
  private IFish fishBehaviour;
  
  public void Setup(FishingRodData rodData, FishData fishData)
  {
    fishBehaviour = SetFishBehaviourStrategy(fishData);
    //...
  }

  private void FixedUpdate()
  {
    //...
    fishBehaviour.Move(ref fishPosition);
    //...
  }

  private IFish SetFishBehaviourStrategy(FishData fishData)
  {
    return fishData.fishType switch
    {
      FishType.Mixed => new MixedFishBehaviour(fishData),
      FishType.Smooth => new SmoothFishBehaviour(fishData),
      FishType.Sinker => new ContinuousMovementFishBehaviour(fishData, ContinuousMovementFishBehaviour.FishContinuousMovementDirection.down),
      FishType.Floater => new ContinuousMovementFishBehaviour(fishData, ContinuousMovementFishBehaviour.FishContinuousMovementDirection.up),
      FishType.Dart => new DartFishBehaviour(fishData),
      _ => new MixedFishBehaviour(fishData)
    };
  }
}

public abstract class FishBehaviour : IFish {}
public class MixedFishBehaviour : FishBehaviour {}
public class ContinuousMovementFishBehaviour : FishBehaviour {}
public class DartFishBehaviour : FishBehaviour {}
```

### Setup scene

As this is only a fragment of the entire game, I divided it into two parts - a menu for selecting fish settings and player statistics, and a  menu of minigame. </br>
The selection UI is the equivalent of the player statistics, specific items that change the behavior of the fishing rod, and specific types of fish.</br>
The UI of the minigame is very similar to the original, the bar has the pixel length given on the main game wiki, the size of the catch bar increases depending on the selected settings. And the UI shakes and spins while trying to catch fish.
