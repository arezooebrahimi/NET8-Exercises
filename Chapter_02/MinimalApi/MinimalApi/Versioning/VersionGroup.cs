namespace MinimalApi.Versioning
{
    public static class VersionGroup
    {
        public static RouteGroupBuilder GroupVersion1(this RouteGroupBuilder group)
        {
            group.MapGet("/hello_world_version_route", () => $"Hello version 1");
            return group;
        }


        public static RouteGroupBuilder GroupVersion2(this RouteGroupBuilder group)
        {
            group.MapGet("/hello_world_version_route", () => $"Hello version 2");
            group.MapGet("/version2only", () => $"Hello version 2 only");
            return group;
        }
    }
}
