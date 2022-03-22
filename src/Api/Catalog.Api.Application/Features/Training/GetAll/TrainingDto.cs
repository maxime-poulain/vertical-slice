﻿namespace Catalog.Api.Application.Features.Training.GetAll;

public class TrainingDto
{
    public int Id { get; set; }

    public int TrainingType { get; set; }

    public string? Description { get; set; }

    public string? Title { get; set; }
}