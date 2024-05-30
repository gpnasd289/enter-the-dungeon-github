public interface IHealth
{
    float Health { get; }
    float HealthCapacity { get; set; }
    void Heal(float health);
    void TakeDamage(float damage, bool canSurvive = false);
    bool Alive { get; }
}
