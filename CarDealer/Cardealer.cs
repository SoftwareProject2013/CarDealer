using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Serializable;
namespace CarDealer
{
    public class Cardealer
    {


        private Serialization vehiculeFile = new Serialization();
        private Serialization customerFile = new Serialization();
        private Serialization contractFile = new Serialization();
        private Serialization leasingFile = new Serialization();

        private List<Contract> contractList = new List<Contract>();
        private List<Leasing> leasingList = new List<Leasing>();
        private List<Customer> customerList = new List<Customer>();
        private List<Vehicule> vehiculeList = new List<Vehicule>();

        public Cardealer()
        {
            try
            {
                loadContract();
            }
            catch (Exception exception)
            {
                saveContract();
            }
            try
            {
                loadCustomer();
            }
            catch (Exception exception)
            {
                saveCustomer();
            }
            try
            {
                loadLeasing();
            }
            catch (Exception exception)
            {
                saveLeasing();
            }
            try
            {
                loadVehicule();
            }
            catch (Exception exception)
            {
                saveVehicule();
            }
        }

        #region leasings
        /// <summary>
        /// method to add leasing to the leasing list and serialise it and change vehicule state to leased
        /// </summary>
        /// <param name="leasing"></param>
        /// <return>void</return>
        public void addLeasing(Leasing leasing)
        {
            try
            {
                leasingList.Add(leasing);
                leasing.myTruck.currentVehiculeState = Vehicule.vehiculeState.leased;
                saveLeasing();
                saveVehicule();
                saveCustomer();
            }
            catch (Exception exception)
            {
                throw (new Exception("Error during adding leasing" + exception.Message));
            }
        }
        /// <summary>
        /// method to create new leasing from params and add call addleasing to add it to the list
        /// </summary>
        /// <param name="endDate"></param>
        /// <param name="startDate"></param>
        /// <param name="myBusiness"></param>
        /// <param name="myTruck"></param>
        /// <return>void</return>
        public void addLeasing(DateTime endDate, DateTime startDate, Business myBusiness ,Truck myTruck)
        {
           addLeasing(new Leasing(endDate,
                    startDate, myBusiness, myTruck));
        }
        /// <summary>
        /// method to remove leasing from leasing list and serialise after changes
        /// </summary>
        /// <param name="leasing"></param>
        public void removeLeasing(Leasing leasing)
        {
            try
            {
                leasingList.Remove(leasing);
                leasing.myTruck.currentVehiculeState = Vehicule.vehiculeState.free;
                saveLeasing();
            }
            catch (Exception exception)
            {
                throw (new Exception("Error while removing leasing" + exception.Message));
            }
        }
        /// <summary>
        /// method returns reference to leasingList
        /// </summary>
        /// <params>none</params>
        /// <returns>list of leasings</returns>
        public List<Leasing> getLeasingsList()
        {
            return leasingList;
        }
        /// <summary>
        /// method to find leasings in leasingList belongs to client by client id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>leasing</returns>
        public List<Leasing> findLeasingsByClientName(string name)
        {
            List<Leasing> clientLeasing = new List<Leasing>();
            foreach (Leasing leasing in leasingList)
            {
                if (leasing.myBusiness.nameCompany == name)
                {
                    clientLeasing.Add(leasing);
                }
            }
            return clientLeasing;
        }
        /// <summary>
        /// serialisation - save leasings list to file
        /// </summary>
        public void saveLeasing()
        {

            try
            {
                this.leasingFile.Enregistrer(this.leasingList, "leasingList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }

        }
        /// <summary>
        /// serialization - loading leasing list from file
        /// </summary>
        public void loadLeasing()
        {


            try
            {
                this.leasingList = this.leasingFile.Charger<List<Leasing>>("leasingList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }
        #endregion

        #region contracts
        /// <summary>
        /// method is adding contract to contractList change vehicule state to commision and call serialization method
        /// </summary>
        /// <param name="contract"></param>
        public void addContract(Contract contract)
        {
            contractList.Add(contract);
            contract.myCar.currentVehiculeState = Vehicule.vehiculeState.commission;
            saveContract();
        }
       /// <summary>
       /// method create new contract object using parameters and call addContract method
       /// </summary>
       /// <param name="startDate"></param>
       /// <param name="endDate"></param>
       /// <param name="privateCustomer"></param>
       /// <param name="myCar"></param>
        public void addContract(DateTime startDate, DateTime endDate, 
                    PrivateCustomer privateCustomer, Car myCar)
        {
            addContract(new Contract(startDate,endDate,privateCustomer,myCar));
            saveContract();
            saveVehicule();
            saveCustomer();
        }
        /// <summary>
        /// method to remove contract from contract list and set leased vehicule state to free and serialize after it
        /// </summary>
        /// <param name="contract"></param>
        public void removeContract(Contract contract)
        {
            try
            {
                contractList.Remove(contract);
                contract.myCar.currentVehiculeState = Vehicule.vehiculeState.free;
                saveContract();
            }
            catch (Exception exception)
            {
                throw(new Exception("error while removing a contract" + exception.Message));
            }
        }
        /// <summary>
        /// contract to get reference to contract list
        /// </summary>
        /// <returns> contracts list</returns>
        public List<Contract> getContractsList()
        {
            return contractList;
        }

        /// <summary>
        /// method returns all contracts belongs to customer by customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Contract> findContractsByClientName(string name)
        {
            List<Contract> clientContracts = new List<Contract>();
            foreach (Contract contract in contractList)
            {
                if (contract.myPrivate.name == name)
                {
                    clientContracts.Add(contract);
                }
            }
            return clientContracts;
        }


        public Contract getContractByVehiculeId(string idVehicule)
        {
            return contractList.Find(c => c.myCar.idVehicule == idVehicule);
        }
        public Leasing getLeasingByVehiculeId(string idVehicule)
        {
            return leasingList.Find(l => l.myTruck.idVehicule == idVehicule);
        }
        /// <summary>
        /// method to serialize contract list to file
        /// </summary>
        public void saveContract()
        {

            try
            {
                this.contractFile.Enregistrer(this.contractList, "contractList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }

        }
        /// <summary>
        /// method to get contract list from file - deserialize
        /// </summary>
        public void loadContract()
        {


            try
            {
                this.contractList = this.contractFile.Charger<List<Contract>>("contractList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }
        #endregion

        #region customers
        /// <summary>
        /// function to add new customer to customer list and serialize customers
        /// </summary>
        /// <param name="customer"></param>
        public void addCustomer(Customer customer)
        {
            customerList.Add(customer);
            saveCustomer();
        }

        /// <summary>
        /// function to remove curent customer froem customers list and serialise them after modification
        /// </summary>
        /// <param name="customer"></param>
        public void removeCustomer(Customer customer)
        {
            customerList.Remove(customer);
            saveCustomer();
        }

        /// <summary>
        /// method to get reference to customer list
        /// </summary>
        /// <returns></returns>
        public List<Customer> getCustomers()
        {
            try
            {
                return customerList;
            }
            catch (Exception exception)
            {
                throw (exception);
            }
        }

        /// <summary>
        /// method find cutomer object in customer list by name or companyName and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Customer getCustomerByName(string name)
        {
            foreach (Customer customer in customerList)
            {
                if (customer is PrivateCustomer && ((PrivateCustomer)customer).name == name)
                {
                    return customer;
                }
                if (customer is Business && ((Business)customer).nameCompany == name)
                {
                    return customer;
                }
            }
            return null;
        }
        /// <summary>
        /// function to serialize customers
        /// </summary>
        public void saveCustomer()
        {

            try
            {
                this.customerFile.Enregistrer(this.customerList, "customerList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }

        }
        /// <summary>
        /// method to deserialize customers
        /// </summary>
        public void loadCustomer()
        {
            try
            {
                this.customerList = this.customerFile.Charger<List<Customer>>("customerList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }
        #endregion

        #region vehicule
        /// <summary>
        /// method to find vehicule in vehiculelist by appropriate id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vehicule getVehiculeById(string id)
        {
                return vehiculeList.Find(v => v.idVehicule == id);          
        }


        /// <summary>
        /// method returns reference to vehicule list
        /// </summary>
        /// <returns></returns>
        public List<Vehicule> getVehicules()
        {
            return vehiculeList;
        }


        /// <summary>
        /// method add vehicule to vehicule list and serialize it after that modification
        /// </summary>
        /// <param name="vehicule"></param>
        public void addVehicule(Vehicule vehicule)
        {
            vehiculeList.Add(vehicule);
            saveVehicule();
        }


        /// <summary>
        /// method to remove vehicile from vehicile list and serialize it after
        /// </summary>
        /// <param name="vehicule"></param>
        public void removeVehicule(Vehicule vehicule)
        {
            vehiculeList.Remove(vehicule);
            saveVehicule();
        }


        /// <summary>
        /// function to get vehicule from vehicule list by appropriate id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vehicule getVehiculeList(string id)
        {
            foreach (Vehicule vehicule in vehiculeList)
            {
                if (vehicule.idVehicule == id)
                {
                    return vehicule;
                }
            }
            return null;
        }


        /// <summary>
        /// function to get all truck whitch are in free state froem truck list
        /// </summary>
        /// <returns></returns>
        public List<Vehicule> getFreeTruckList()
        {
            return vehiculeList.FindAll(v => (v is Truck && v.currentVehiculeState == Vehicule.vehiculeState.free) );
        }


        /// <summary>
        /// method to serialize vehicules to the file
        /// </summary>
        public void saveVehicule()
        {

            try
            {
                this.vehiculeFile.Enregistrer(this.vehiculeList, "vehiculeList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }

        }
        /// <summary>
        /// method to deserialize vehiciles froem appropriate file
        /// </summary>
        public void loadVehicule()
        {
            try
            {
                this.vehiculeList = this.vehiculeFile.Charger<List<Vehicule>>("vehiculeList.bin");
            }
            catch (Exception e1)
            {
                throw e1;
            }
        }
        /// <summary>
        /// method returns all cars whitch are in state free
        /// </summary>
        /// <returns></returns>
        public List<Vehicule> getFreeCarsList()
        {
            return vehiculeList.FindAll(v => (v is Car && v.currentVehiculeState == Vehicule.vehiculeState.free) );
        }
        /// <summary>
        /// method returns all small cars whitch are in state free
        /// </summary>
        /// <returns></returns>
        public List<Vehicule> getFreeSmallCarsList()
        {
            return vehiculeList.FindAll(v => (v is Small && v.currentVehiculeState == Vehicule.vehiculeState.free));
        }
        /// <summary>
        /// method returns all large cars whitch are in state free
        /// </summary>
        /// <returns></returns>
        public List<Vehicule> getFreeLargeCarsList()
        {
            return vehiculeList.FindAll(v => (v is Large && v.currentVehiculeState == Vehicule.vehiculeState.free));
        }
        #endregion



    }




}
