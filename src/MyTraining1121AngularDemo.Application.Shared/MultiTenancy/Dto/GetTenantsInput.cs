using System;
using Abp.Runtime.Validation;
using MyTraining1121AngularDemo.Common;
using MyTraining1121AngularDemo.Dto;

namespace MyTraining1121AngularDemo.MultiTenancy.Dto
{
    public class GetTenantsInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public string Filter { get; set; }
        public DateTime? SubscriptionEndDateStart { get; set; }
        public DateTime? SubscriptionEndDateEnd { get; set; }
        public DateTime? CreationDateStart { get; set; }
        public DateTime? CreationDateEnd { get; set; }
        public int? EditionId { get; set; }
        public bool EditionIdSpecified { get; set; }

        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "TenancyName";
            }

            Sorting = DtoSortingHelper.ReplaceSorting(Sorting, s =>
            {
                return s.Replace("editionDisplayName", "Edition.DisplayName");
            });
        }
    }
}

