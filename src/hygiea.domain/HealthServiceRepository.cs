namespace hygiea.domain
{
    public interface HealthServiceRepository
    {
        HealthService GetBy(string id);
    }    
}