using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int experience = 0;
    [SerializeField] private int experienceToNextLevel = 100;

    public int CurrentLevel => currentLevel;
    public int Experience => experience;
    public int ExperienceToNextLevel => experienceToNextLevel;

    public void AddExperience(int amount)
    {
        experience += amount;
        
        while (experience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        currentLevel++;
        experience -= experienceToNextLevel;
        experienceToNextLevel = (int)(experienceToNextLevel * 1.5f); // Increase XP needed for next level
        
        Debug.Log($"Level Up! Now level {currentLevel}");
        // TODO: Show level up UI and options
    }
} 