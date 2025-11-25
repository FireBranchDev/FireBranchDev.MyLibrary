using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FireBranchDev.MyLibrary.Persistence.ValueConverters;

public class DateTimeUtcValueConverter : ValueConverter<DateTime, DateTime>
{
    public DateTimeUtcValueConverter() : base(
        d => d.ToUniversalTime(),
        d => DateTime.SpecifyKind(d, DateTimeKind.Utc).ToLocalTime())
    {
        
    }
}
