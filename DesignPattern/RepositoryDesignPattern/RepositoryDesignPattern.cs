using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
What is the Repository Design Pattern in C#?
- The Repository Design Pattern in C# Mediates between the domain and the data mapping layers using a collection-like interface 
  for accessing the domain objects.
- In other words, we can say that a Repository Design Pattern acts as a 
  middleman or middle layer between the rest of the application and the data access logic.
- That means a repository pattern isolates all the data access code from the rest of the application.
- The advantage of doing so is that, if you need to do any changes then you need to do it in one place.
- nowadays, most data-driven applications need to access the data residing in one or more other data sources. 
  Most of the time data sources will be a database. 

 */

namespace DesignPattern.RepositoryDesignPattern
{
    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public Nullable<int> Salary { get; set; }
        public string Dept { get; set; }
    }
    public partial class EmployeeDBContext : DbContext
    {
        //public EmployeeDBContext()
        //    : base("name=EmployeeDBContext")
        //{
        //}
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new UnintentionalCodeFirstException();
        //}
        public virtual DbSet<Employee> Employees { get; set; }
    }
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int EmployeeID);
        void Insert(Employee employee);
        void Update(Employee employee);
        void Delete(int EmployeeID);
        void Save();
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDBContext _context;
        public EmployeeRepository()
        {
            _context = new EmployeeDBContext();
        }
        public EmployeeRepository(EmployeeDBContext context)
        {
            _context = context;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }
        public Employee GetById(int EmployeeID)
        {
            return _context.Employees.Find(EmployeeID);
        }
        public void Insert(Employee employee)
        {
            _context.Employees.Add(employee);
        }
        public void Update(Employee employee)
        {
            _context.Entry(employee).State = EntityState.Modified;
        }
        public void Delete(int EmployeeID)
        {
            Employee employee = _context.Employees.Find(EmployeeID);
            _context.Employees.Remove(employee);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    /*
     Why do we need Generic Repository Pattern in c#?
    - in Basic Repository, you need to create separate repositories for each and every entity present in your application. 
    - For example, if you have three entities such as Employee, Product, and Customer, 
      then you need to create three repositories as EmployeeRepository, ProductRepository, and CustomerRepository.
     */
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EmployeeDBContext _context = null;
        private DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new EmployeeDBContext();
            table = _context.Set<T>();
        }
        public GenericRepository(EmployeeDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }

    public class EmployeeForGenericRPController
    {
        private IGenericRepository<Employee> repository = null;
        public EmployeeForGenericRPController()
        {
            this.repository = new GenericRepository<Employee>();
        }
        public EmployeeForGenericRPController(IGenericRepository<Employee> repository)
        {
            this.repository = repository;
        }

        public static void Main()
        {
            //var employeesAll = repository.GetAll();

            //Employee employee = repository.GetById(EmployeeId);

            //repository.Insert(model);
            //repository.Save();

            //repository.Update(model);
            //repository.Save();

            //repository.Delete(EmployeeID);
            //repository.Save();
        }
    }
    class EmployeeController
    {
        private IEmployeeRepository _employeeRepository;
        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository(new EmployeeDBContext());
        }
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public static void Main()
        {
            //Get All
            //var employeeList = _employeeRepository.GetAll();

            //Get By Id
            //Employee model = _employeeRepository.GetById(EmployeeId);

            //Post
            //if (ModelState.IsValid)
            //{
                //_employeeRepository.Insert(model);
                //_employeeRepository.Save();
            //}

            //Put
            //if (ModelState.IsValid)
            //{
                //_employeeRepository.Update(model);
                //_employeeRepository.Save();
            //}

            //Delete
            //if (ModelState.IsValid)
            //{
                //_employeeRepository.Delete(EmployeeID);
                //_employeeRepository.Save();
            //}
        }
    }
}

namespace DesignPattern.RepositoryDesignPattern.BothGenericAndNonGeneric
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(object id);
        void Save();
    }

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public EmployeeDBContext _context = null;
        public DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new EmployeeDBContext();
            table = _context.Set<T>();
        }
        public GenericRepository(EmployeeDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }

    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByGender(string Gender);
        IEnumerable<Employee> GetEmployeesByDepartment(string Dept);
    }

    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public IEnumerable<Employee> GetEmployeesByGender(string Gender)
        {
            return _context.Employees.Where(emp => emp.Gender == Gender).ToList();
        }
        public IEnumerable<Employee> GetEmployeesByDepartment(string Dept)
        {
            return _context.Employees.Where(emp => emp.Dept == Dept).ToList();
        }
    }

    public class EmployeeController
    {
        private IGenericRepository<Employee> repository = null;
        private IEmployeeRepository employee_repository = null;
        public EmployeeController()
        {
            this.employee_repository = new EmployeeRepository();
            this.repository = new GenericRepository<Employee>();
        }
        public EmployeeController(EmployeeRepository repository)
        {
            this.employee_repository = repository;
        }
        public EmployeeController(IGenericRepository<Employee> repository)
        {
            this.repository = repository;
        }

        public static void Main()
        {
            //var allMaleEmployees = employee_repository.GetEmployeesByGender("Male");

            //Employee model = repository.GetById(EmployeeId);

            //repository.Insert(model);
            //repository.Save();

            //repository.Update(model);
            //repository.Save();

            //repository.Delete(EmployeeID);
            //repository.Save();
        }
    }
}


namespace DesignPattern.RepositoryDesignPattern.UnitOfWorkInRepositoryPattern
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public EmployeeDBContext _context = null;
        public DbSet<T> table = null;
        public GenericRepository()
        {
            this._context = new EmployeeDBContext();
            table = _context.Set<T>();
        }
        public GenericRepository(EmployeeDBContext _context)
        {
            this._context = _context;
            table = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public T GetById(object id)
        {
            return table.Find(id);
        }
        public void Insert(T obj)
        {
            table.Add(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
        public void Delete(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }

    public interface IUnitOfWork<out TContext>
        where TContext : DbContext, new()
    {
        TContext Context { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }

    public class UnitOfWork<TContext> : IUnitOfWork<TContext>, IDisposable
         where TContext : DbContext, new()
    {
        //Here TContext is nothing but your DBContext class
        //In our example it is EmployeeDBContext class
        private readonly TContext _context;
        private bool _disposed;
        private string _errorMessage = string.Empty;
        private DbContextTransaction _objTran;
        private Dictionary<string, object> _repositories;
        //Using the Constructor we are initializing the _context variable is nothing but
        //we are storing the DBContext (EmployeeDBContext) object in _context variable
        public UnitOfWork()
        {
            _context = new TContext();
        }
        //The Dispose() method is used to free unmanaged resources like files, 
        //database connections etc. at any time.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //This Context property will return the DBContext object i.e. (EmployeeDBContext) object
        public TContext Context
        {
            get { return _context; }
        }
        //This CreateTransaction() method will create a database Trnasaction so that we can do database operations by
        //applying do evrything and do nothing principle
        public void CreateTransaction()
        {
            _objTran = _context.Database.BeginTransaction();
        }
        //If all the Transactions are completed successfuly then we need to call this Commit() 
        //method to Save the changes permanently in the database
        public void Commit()
        {
            _objTran.Commit();
        }
        //If atleast one of the Transaction is Failed then we need to call this Rollback() 
        //method to Rollback the database changes to its previous state
        public void Rollback()
        {
            _objTran.Rollback();
            _objTran.Dispose();
        }
        //This Save() Method Implement DbContext Class SaveChanges method so whenever we do a transaction we need to
        //call this Save() method so that it will make the changes in the database
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _context.Dispose();
            _disposed = true;
        }

        public GenericRepository<T> GenericRepository<T>() where T : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, object>();
            var type = typeof(T).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<T>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }
            return (GenericRepository<T>)_repositories[type];
        }
    }
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
    }

    public class GenericRepository<T> : IGenericRepository<T>, IDisposable where T : class
    {
        private IDbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
        public GenericRepository(IUnitOfWork<EmployeeDBContext> unitOfWork)
            : this(unitOfWork.Context)
        {
        }
        public GenericRepository(EmployeeDBContext context)
        {
            _isDisposed = false;
            Context = context;
        }
        public EmployeeDBContext Context { get; set; }
        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }
        protected virtual IDbSet<T> Entities
        {
            get { return _entities ?? (_entities = Context.Set<T>()); }
        }
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }
        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }
        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                Entities.Add(entity);
                if (Context == null || _isDisposed)
                    Context = new EmployeeDBContext();
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be 
                //called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public void BulkInsert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                {
                    throw new ArgumentNullException("entities");
                }
                Context.Configuration.AutoDetectChangesEnabled = false;
                Context.Set<T>().AddRange(entities);
                Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage += string.Format("Property: {0} Error: {1}", validationError.PropertyName,
                                             validationError.ErrorMessage) + Environment.NewLine;
                    }
                }
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new EmployeeDBContext();
                SetEntryModified(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new EmployeeDBContext();
                Entities.Remove(entity);
                //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        _errorMessage += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                throw new Exception(_errorMessage, dbEx);
            }
        }
        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }
    }

    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeesByGender(string Gender);
        IEnumerable<Employee> GetEmployeesByDepartment(string Dept);
    }

    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IUnitOfWork<EmployeeDBContext> unitOfWork)
            : base(unitOfWork)
        {
        }
        public EmployeeRepository(EmployeeDBContext context)
            : base(context)
        {
        }
        public IEnumerable<Employee> GetEmployeesByGender(string Gender)
        {
            return Context.Employees.Where(emp => emp.Gender == Gender).ToList();
        }
        public IEnumerable<Employee> GetEmployeesByDepartment(string Dept)
        {
            return Context.Employees.Where(emp => emp.Dept == Dept).ToList();
        }
    }

    public class EmployeeController
    {
        private UnitOfWork<EmployeeDBContext> unitOfWork = new UnitOfWork<EmployeeDBContext>();
        private GenericRepository<Employee> repository;
        private IEmployeeRepository employeeRepository;
        public EmployeeController()
        {
            //If you want to use Generic Repository with Unit of work
            repository = new GenericRepository<Employee>(unitOfWork);
            //If you want to use Specific Repository with Unit of work
            employeeRepository = new EmployeeRepository(unitOfWork);
        }

        public static void Main()
        {
            //var model = repository.GetAll();

            //Employee model = repository.GetById(EmployeeId);

            //unitOfWork.CreateTransaction();
            //repository.Insert(model);
            //unitOfWork.Save();
            //unitOfWork.Commit();


            //repository.Update(model);
            //unitOfWork.Save();

            //repository.Delete(model);
            //unitOfWork.Save();
        }
    }
}