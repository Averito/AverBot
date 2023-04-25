using AverBot.Core.Domain.Enums;

namespace AverBot.Core.DTO;

public class CreatePunishmentDTO
{
    public TypeOfViolation TypeOfViolation { get; }
    public PunishmentType PunishmentType { get; }
    public int TimeInSeconds { get; }
    public int ViolationCount { get; }
    public string? Comment { get; }

    public CreatePunishmentDTO(TypeOfViolation typeOfViolation, PunishmentType punishmentType, int violationCount, int timeInSeconds = 3600,
        string? comment = null)
    {
        TypeOfViolation = typeOfViolation;
        ViolationCount = violationCount;
        PunishmentType = punishmentType;
        Comment = comment;
        TimeInSeconds = timeInSeconds;
    }
}