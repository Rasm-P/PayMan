using PayManAPI.Dtos;
using PayManAPI.Models;

namespace PayManAPI
{
    //Use of extention method
    public static class Extentions
    {
        //Converts a User object to a UserDto
        public static UserDto AsUserDto(this UserModel user)
        {
            //Needs code for non empty JobDto list
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Frikort = user.Frikort,
                Hovedkort = user.Hovedkort,
                CreatedAt = user.CreatedAt,
            };
        }

        //Converts a Job object to a JobDto
        public static JobDto AsJobDto(this JobModel job)
        {
            return new JobDto
            {
                Id = job.Id,
                JobTitle = job.JobTitle,
                Description = job.Description,
                HourlyWage = job.HourlyWage,
                Taxes = job.Taxes,
                WorkHours = job.WorkHours,
                CreatedAt = job.CreatedAt
            };
        }

        //Converts a Tax object to a TaxDto
        public static TaxDto AsTaxDto(this TaxModel tax)
        {
            return new TaxDto
            {
                Id = tax.Id,
                TaxName = tax.TaxName,
                Description = tax.Description,
                TaxRate = tax.TaxRate,
                CreatedAt = tax.CreatedAt
            };
        }

        //Converts a WorkHour object to a WorkHourDto
        public static WorkHourDto AsWorkHourDto(this WorkHourModel workHour)
        {
            return new WorkHourDto
            {
                Id = workHour.Id,
                WorkStart = workHour.WorkStart,
                WorkEnd = workHour.WorkEnd,
                Description = workHour.Description,
                CreatedAt = workHour.CreatedAt
            };
        }
    }
}
