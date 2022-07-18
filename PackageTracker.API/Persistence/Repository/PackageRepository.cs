using Microsoft.EntityFrameworkCore;
using PackageTracker.API.Entities;

namespace PackageTracker.API.Persistence.Repository
{
  public class PackageRepository : IPackageRepository
  {
    private readonly PackageTrackerContext _context;
    public PackageRepository(PackageTrackerContext context)
    {
      _context = context;
    }
    public Package GetPackageByCode(string code)
    {
      return _context
      .Packages
      .Include(p => p.Updates)
      .SingleOrDefault(x=> x.Code == code);
    }

    List<Package> IPackageRepository.GetAllPackages()
    {
      return _context
      .Packages
      .Include(p => p.Updates)
      .ToList()
      .FindAll(x=> x.Actived == true);
    }

    public void AddPackage(Package package)
    {
      _context.Packages.Add(package);
      _context.SaveChanges();
    }

    public void AddUpdatePackage(Package package)
    {
      _context.Entry(package).State = EntityState.Modified;
      _context.SaveChanges();
    }

    public void DeletePackage(Package package)
    {
      package.Actived = false;
      _context.SaveChanges();
    }

    public void UpdatePackage(Package package)
    {
      _context.Entry(package).State = EntityState.Modified;
      _context.SaveChanges();
    }
  }
}