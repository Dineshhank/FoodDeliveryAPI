namespace FoodDelivery.Application.Common.Geo
{
    public static class GeoDistance
    {
        /// <summary>Great-circle distance in meters (WGS84 sphere approximation).</summary>
        public static double MetersBetween(decimal lat1, decimal lon1, decimal lat2, decimal lon2) =>
            MetersBetween((double)lat1, (double)lon1, (double)lat2, (double)lon2);

        public static double MetersBetween(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadiusM = 6371000d;
            var dLat = (lat2 - lat1) * (Math.PI / 180d);
            var dLon = (lon2 - lon1) * (Math.PI / 180d);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * (Math.PI / 180d)) * Math.Cos(lat2 * (Math.PI / 180d)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return earthRadiusM * c;
        }
    }
}
