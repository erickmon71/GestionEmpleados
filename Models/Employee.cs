using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionEmpleados.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [StringLength(100, ErrorMessage = "El apellido no puede exceder 100 caracteres.")]
    public string LastName { get; set; }

    [StringLength(255, ErrorMessage = "La dirección no puede exceder 255 caracteres.")]
    public string Address { get; set; }

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido.")]
    [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder 100 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
    [Phone(ErrorMessage = "Formato de número de teléfono inválido.")]
    [StringLength(20, ErrorMessage = "El número de teléfono no puede exceder 20 caracteres.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de fecha inválido.")]
    [Display(Name = "Fecha de Nacimiento")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "La fecha de contratación es obligatoria.")]
    [DataType(DataType.Date, ErrorMessage = "Formato de fecha inválido.")]
    [Display(Name = "Fecha de Contratación")]
    [DateGreaterThan("DateOfBirth", ErrorMessage = "La fecha de contratación debe ser posterior a la fecha de nacimiento.")]

    public DateTime DateOfHire { get; set; }

    public virtual ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

    public virtual ICollection<PerformanceReview> PerformanceReviewEmployees { get; set; } = new List<PerformanceReview>();

    public virtual ICollection<PerformanceReview> PerformanceReviewReviewers { get; set; } = new List<PerformanceReview>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}


public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = (DateTime)value;

        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (property == null)
        {
            throw new ArgumentException("Property with this name not found");
        }

        var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

        if (currentValue <= comparisonValue)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}