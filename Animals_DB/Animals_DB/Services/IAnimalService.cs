using Animals_DB.Models;

namespace Animals_DB.Services;

public interface IAnimalService
{
    IEnumerable<Animal> GetAnimals(string orderBy);
    Animal? GetAnimal(int animalId);
    int AddAnimal(Animal animal);
    int UpdateAnimal(Animal animal);
    int DeleteAnimal(int animalId);
}