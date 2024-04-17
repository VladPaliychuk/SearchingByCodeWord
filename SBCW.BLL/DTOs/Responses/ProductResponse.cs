﻿namespace SBCW.BLL.DTOs.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public string? Link { get; set; }
    public string Type { get; set; }
}