using Animals_DB.Models;
using Animals_DB.Repositories;

namespace Animals_DB.Services;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public IEnumerable<Animal> GetAnimals(string orderBy)
    {
        return _animalRepository.GetAnimals(checkOrderBy(orderBy));
    }

    public Animal? GetAnimal(int animalId)
    {
        return _animalRepository.GetAnimal(animalId);
    }

    public int AddAnimal(Animal animal)
    {
        return _animalRepository.AddAnimal(animal);
    }

    public int UpdateAnimal(Animal animal)
    {
        return _animalRepository.UpdateAnimal(animal);
    }

    public int DeleteAnimal(Animal animal)
    {
        throw new NotImplementedException();
    }

    private string checkOrderBy(string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy)||(orderBy.ToLower() != "name" && orderBy.ToLower() != "description" && orderBy.ToLower() != "category" 
                                             && orderBy.ToLower() != "area"))
            {
                return "name";
            }
            
            return orderBy;
    }
}