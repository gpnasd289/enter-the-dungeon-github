public interface IHealth
{
    int Health { get; }
    int HealthCapacity { get; set; }
    void Heal(int health);
    void TakeDamage(int damage, bool canSurvive = false);
    bool Alive { get; }
}
