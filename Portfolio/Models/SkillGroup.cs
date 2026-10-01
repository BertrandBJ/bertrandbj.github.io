namespace Portfolio.Models;

public record SkillGroup(string Category, IReadOnlyList<string> Skills);

// same as : public record SkillGroup
// {
//     public string Category { get; init; }
//     public IReadOnlyList<string> Skills { get; init; }
// 
//     public SkillGroup(string category, IReadOnlyList<string> skills)
//     {
//         Category = category;
//         Skills = skills;
//     }
// }
// le record qui fait du heavy lifting.