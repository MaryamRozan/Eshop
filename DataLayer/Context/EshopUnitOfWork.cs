
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class EshopUnitOfWork : IDisposable
    {
        MyEshop_DBEntities db = new MyEshop_DBEntities();
        IGenericRepository<Users> _AccountrRepository;
        public IGenericRepository<Users> AccountRepository
        {
            get
            {
                if (_AccountrRepository == null)
                {
                    _AccountrRepository = new GenericRepository<Users>(db);
                }
                return _AccountrRepository;
            }

        }

        IGenericRepository<Roles> _rolesRepository;
        public IGenericRepository<Roles> RolesRepository
        {
            get
            {
                if (_rolesRepository == null)
                {
                    _rolesRepository = new GenericRepository<Roles>(db);
                }
                return _rolesRepository;
            }
        }

        IUsersRepository _userrRepository;
        public IUsersRepository UserRepository {
            get {
                if (_userrRepository == null)
                {
                    _userrRepository = new UserRepository(db);
                }
                return _userrRepository;
            }
        }

        IGenericRepository<Product_Groups> _productGroupRepository;
        public IGenericRepository<Product_Groups> ProductGroupRepository {
            get {
                if (_productGroupRepository == null) {
                    _productGroupRepository = new GenericRepository<Product_Groups>(db);  
                }
                return _productGroupRepository;
            }
        }


        IGenericRepository<Products> _productRepository;
        public IGenericRepository<Products> ProductRepository {
            get {
                if (_productGroupRepository == null) {
                    _productRepository = new GenericRepository<Products>(db);
                }
                return _productRepository;
            }
        }

        IGenericRepository<SelectedProductGroup> _selectedGroupRepository;
        public IGenericRepository<SelectedProductGroup> SelectedGroupRepository {
            get {
                if (_selectedGroupRepository == null) {
                    _selectedGroupRepository = new GenericRepository<SelectedProductGroup>(db);
                }
                return _selectedGroupRepository;
            }
        }

        IGenericRepository<Tags> _tagsRepository;
        public IGenericRepository<Tags> TagsRepository {
            get {
                if (_tagsRepository == null) {
                    _tagsRepository = new GenericRepository<Tags>(db);
                 }
                return _tagsRepository;
            }
        }

        IGenericRepository<Product_Galleries> _galleryRepository;
        public IGenericRepository<Product_Galleries> GalleryRepository
        {
            get
            {
                if (_galleryRepository == null)
                {
                    _galleryRepository = new GenericRepository<Product_Galleries>(db);
                }
                return _galleryRepository;
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public void Save() {
            db.SaveChanges();
        }
    }
}
