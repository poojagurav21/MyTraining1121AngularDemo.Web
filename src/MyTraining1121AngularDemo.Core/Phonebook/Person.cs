﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTraining1121AngularDemo.Phonebook;

[Table("PbPersons")]
public class Person : FullAuditedEntity,IMustHaveTenant
{
    public const int MaxNameLength = 32;
    public const int MaxSurnameLength = 32;
    public const int MaxEmailAddressLength = 255;
    public virtual int TenantId { get; set; }
    [Required]
    [MaxLength(MaxNameLength)]
    public virtual string Name { get; set; }

    [Required]
    [MaxLength(MaxSurnameLength)]
    public virtual string Surname { get; set; }

    [MaxLength(MaxEmailAddressLength)]
    public virtual string EmailAddress { get; set; }
    public virtual ICollection<Phone> Phones { get; set; }
}