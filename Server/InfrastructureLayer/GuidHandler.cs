using System.Data;
using Dapper;

namespace InfrastructureLayer
{
    public class GuidHandler : SqlMapper.TypeHandler<Guid>
    {
        public override void SetValue(IDbDataParameter parameter, Guid guid)
        {
            parameter.Value = guid.ToString();
        }

        public override Guid Parse(object value)
        {
            return new Guid((string)value);
        }
    }
    
    public class DateTimeHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        private readonly TimeZoneInfo databaseTimeZone = TimeZoneInfo.Local;
        public static readonly DateTimeHandler Default = new DateTimeHandler();

        public DateTimeHandler()
        {

        }

        public override DateTimeOffset Parse(object value)
        {
            DateTime storedDateTime;
            if (value == null)
                storedDateTime = DateTime.MinValue;
            else
                storedDateTime = (DateTime)value;

            if (storedDateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime)
                return DateTimeOffset.MinValue;
            else
                return new DateTimeOffset(storedDateTime, databaseTimeZone.BaseUtcOffset);
        }

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
        {
            DateTime paramVal = value.ToOffset(this.databaseTimeZone.BaseUtcOffset).DateTime;
            parameter.Value = paramVal;
        }
    }
}