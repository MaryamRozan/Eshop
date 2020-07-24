
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
        public IUsersRepository UserRepository
        {
            get
            {
                if (_userrRepository == null)
                {
                    _userrRepository = new UserRepository(db);
                }
                return _userrRepository;
            }
        }

        IGenericRepository<Product_Groups> _productGroupRepository;
        public IGenericRepository<Product_Groups> ProductGroupRepository
        {
            get
            {
                if (_productGroupRepository == null)
                {
                    _productGroupRepository = new GenericRepository<Product_Groups>(db);
                }
                return _productGroupRepository;
            }
        }


        IGenericRepository<Products> _productRepository;
        public IGenericRepository<Products> ProductRepository
        {
            get
            {
                if (_productGroupRepository == null)
                {
                    _productRepository = new GenericRepository<Products>(db);
                }
                return _productRepository;
            }
        }

        IGenericRepository<SelectedProductGroup> _selectedGroupRepository;
        public IGenericRepository<SelectedProductGroup> SelectedGroupRepository
        {
            get
            {
                if (_selectedGroupRepository == null)
                {
                    _selectedGroupRepository = new GenericRepository<SelectedProductGroup>(db);
                }
                return _selectedGroupRepository;
            }
        }

        IGenericRepository<Tags> _tagsRepository;
        public IGenericRepository<Tags> TagsRepository
        {
            get
            {
                if (_tagsRepository == null)
                {
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

        IGenericRepository<Features> _featureRepository;
        public IGenericRepository<Features> FeatureRepository
        {
            get
            {
                if (_featureRepository == null)
                {
                    _featureRepository = new GenericRepository<Features>(db);
                }
                return _featureRepository;
            }
        }

        IGenericRepository<Product_Features> _productFeaturesRepository;
        public IGenericRepository<Product_Features> ProductFeaturesRepository
        {
            get
            {
                if (_productFeaturesRepository == null)
                {
                    _productFeaturesRepository = new GenericRepository<Product_Features>(db);
                }
                return _productFeaturesRepository;
            }
        }

        IGenericRepository<Product_Comment> _commentRepository;
        public IGenericRepository<Product_Comment> CommentRepository {
            get {
                if (_commentRepository == null) {
                    _commentRepository = new GenericRepository<Product_Comment>(db);
                }
                return _commentRepository;
            }
        }

        IGenericRepository<Orders> _ordersRepository;
        public IGenericRepository<Orders> OrdersRepository {
            get {
                if (_ordersRepository == null) {
                    _ordersRepository = new GenericRepository<Orders>(db);
                }
                return _ordersRepository;
            }
        }

        IGenericRepository<OrderDetails> _orderDetailsRepository;
        public IGenericRepository<OrderDetails> OrderDetailsRepository
        {
            get {
                if (_orderDetailsRepository == null) {
                    _orderDetailsRepository = new GenericRepository<OrderDetails>(db);
                }
                return _orderDetailsRepository;
            }
        }
        public void Dispose()
        {
            db.Dispose();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
