using PackageTracker.API.Entities;

namespace PackageTracker.API.Persistence.Repository
{
    public interface IPackageRepository
    {
          List<Package> GetAllPackages();
          Package GetPackageByCode(string code);
          void AddPackage(Package package);
          void UpdatePackage(Package package);
          void AddUpdatePackage(Package package);
          void DeletePackage(Package package);
    }
}