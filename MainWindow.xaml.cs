using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoLotModel;

namespace Barbu_Nicoleta_Lab7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        AutoLotEntitiesModel ctx = new AutoLotEntitiesModel();
        CollectionViewSource customerViewSource;
        CollectionViewSource inventoryViewSource;
        CollectionViewSource customerOrdersViewSource;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            customerViewSource.Source = ctx.Customers.Local;
            ctx.Customers.Load();

            inventoryViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("inventoryViewSource")));
            inventoryViewSource.Source = ctx.Inventories.Local;
            ctx.Orders.Load();

            customerOrdersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerOrdersViewSource")));
            customerOrdersViewSource.Source = ctx.Orders.Local;
            ctx.Inventories.Load();

            cmbCustomers.ItemsSource = ctx.Customers.Local;
            cmbCustomers.DisplayMemberPath = "FirstName";
            cmbCustomers.SelectedValuePath = "CustId";
            cmbInventory.ItemsSource = ctx.Inventories.Local;
            cmbInventory.DisplayMemberPath = "Make";
            cmbInventory.SelectedValuePath = "CarId";

        }





        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    customer = new Customer()
                    {
                        FirstName = firstNameTextBox1.Text.Trim(),
                        LastName = lastNameTextBox1.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Customers.Add(customer);
                    customerViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                btnDelete.IsEnabled = true;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                firstNameTextBox1.IsEnabled = false;
                lastNameTextBox1.IsEnabled = false;
               
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    customer.FirstName = firstNameTextBox1.Text.Trim();
                    customer.LastName = lastNameTextBox1.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                firstNameTextBox1.IsEnabled = false;
                lastNameTextBox1.IsEnabled = false;

            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    customer = (Customer)customerDataGrid.SelectedItem;
                    ctx.Customers.Remove(customer);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNew.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
                btnSave.IsEnabled = false;
                btnCancel.IsEnabled = false;
                btnPrev.IsEnabled = true;
                btnNext.IsEnabled = true;
                firstNameTextBox1.IsEnabled = false;
                lastNameTextBox1.IsEnabled = false;

            }
        }

        private void btnSaveI_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Customer entity
                    inventory = new Inventory()
                    {
                        Color = colorTextBox.Text.Trim(),
                        Make = makeTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Inventories.Add(inventory);
                    inventoryViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                btnNewI.IsEnabled = true;
                btnEditI.IsEnabled = true;
                btnSaveI.IsEnabled = false;
                btnCancelI.IsEnabled = false;
                btnDeleteI.IsEnabled = true;
                btnPrevI.IsEnabled = true;
                btnNextI.IsEnabled = true;
                colorTextBox.IsEnabled = false;
                makeTextBox.IsEnabled = false;
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    inventory = (Inventory)inventoryDataGrid.SelectedItem;
                    inventory.Color = colorTextBox.Text.Trim();
                    inventory.Make = makeTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                inventoryViewSource.View.Refresh();
                // pozitionarea pe item-ul curent
                inventoryViewSource.View.MoveCurrentTo(inventory);
                btnNewI.IsEnabled = true;
                btnEditI.IsEnabled = true;
                btnDeleteI.IsEnabled = true;
                btnSaveI.IsEnabled = false;
                btnCancelI.IsEnabled = false;
                btnPrevI.IsEnabled = true;
                btnNextI.IsEnabled = true;
                colorTextBox.IsEnabled = false;
                makeTextBox.IsEnabled = false;
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    inventory = (Inventory)inventoryDataGrid.SelectedItem;
                    ctx.Inventories.Remove(inventory);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                inventoryViewSource.View.Refresh();
                btnNewI.IsEnabled = true;
                btnEditI.IsEnabled = true;
                btnDeleteI.IsEnabled = true;
                btnSaveI.IsEnabled = false;
                btnCancelI.IsEnabled = false;
                btnPrevI.IsEnabled = true;
                btnNextI.IsEnabled = true;
                colorTextBox.IsEnabled = false;
                makeTextBox.IsEnabled = false;
            }
        }

        private void btnSaveO_Click(object sender, RoutedEventArgs e)
        {
            Order order = null;
            if (action == ActionState.New)
            {
                try
                {
                    Customer customer = (Customer)cmbCustomers.SelectedItem;
                    Inventory inventory = (Inventory)cmbInventory.SelectedItem;
                    //instantiem Order entity
                    order = new Order()
                    {

                        CustId = customer.CustId,
                        CarId = inventory.CarId
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Orders.Add(order);
                    customerOrdersViewSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnNewO.IsEnabled = true;
                btnEditO.IsEnabled = true;
                btnSaveO.IsEnabled = false;
                btnCancelO.IsEnabled = false;
                btnDeleteO.IsEnabled = true;
                btnPrevO.IsEnabled = true;
                btnNextO.IsEnabled = true;
            }
            else
            if (action == ActionState.Edit)
            {
                try
                {
                    Customer customer = (Customer)cmbCustomers.SelectedItem;
                    Inventory inventory = (Inventory)cmbInventory.SelectedItem;
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }

   
                btnNewO.IsEnabled = true;
                btnEditO.IsEnabled = true;
                btnDeleteO.IsEnabled = true;
                btnSaveO.IsEnabled = false;
                btnCancelO.IsEnabled = false;
                btnPrevO.IsEnabled = true;
                btnNextO.IsEnabled = true;
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    Customer customer = (Customer)cmbCustomers.SelectedItem;
                    Inventory inventory = (Inventory)cmbInventory.SelectedItem;
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                inventoryViewSource.View.Refresh();
                btnNewO.IsEnabled = true;
                btnEditO.IsEnabled = true;
                btnDeleteO.IsEnabled = true;
                btnSaveO.IsEnabled = false;
                btnCancelO.IsEnabled = false;
                btnPrevO.IsEnabled = true;
                btnNextO.IsEnabled = true;

            }
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            customerViewSource.View.MoveCurrentToNext();
        }


        private void btnPrevI_Click(object sender, RoutedEventArgs e)
        {
            inventoryViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNextI_Click(object sender, RoutedEventArgs e)
        {
            inventoryViewSource.View.MoveCurrentToNext();
        }

        private void btnPrevO_Click(object sender, RoutedEventArgs e)
        {
            customerOrdersViewSource.View.MoveCurrentToPrevious();
        }

        private void btnNextO_Click(object sender, RoutedEventArgs e)
        {
            customerOrdersViewSource.View.MoveCurrentToNext();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            firstNameTextBox1.IsEnabled = true;
            lastNameTextBox1.IsEnabled = true;
            firstNameTextBox1.Text = "";
            lastNameTextBox1.Text = "";
            Keyboard.Focus(firstNameTextBox1);
        }

        private void btnNewI_Click_1(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNewI.IsEnabled = false;
            btnEditI.IsEnabled = false;
            btnDeleteI.IsEnabled = false;
            btnSaveI.IsEnabled = true;
            btnCancelI.IsEnabled = true;
            btnPrevI.IsEnabled = false;
            btnNextI.IsEnabled = false;
            colorTextBox.IsEnabled = true;
            makeTextBox.IsEnabled = true;
            colorTextBox.Text = "";
            makeTextBox.Text = "";
            Keyboard.Focus(colorTextBox);
        }

        private void btnNewO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
            btnNewO.IsEnabled = false;
            btnEditO.IsEnabled = false;
            btnDeleteO.IsEnabled = false;
            btnSaveO.IsEnabled = true;
            btnCancelO.IsEnabled = true;
            btnPrevO.IsEnabled = false;
            btnNextO.IsEnabled = false;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempfirstNameTextBox1 = firstNameTextBox1.Text.ToString();
            string templastNameTextBox1 = lastNameTextBox1.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            firstNameTextBox1.IsEnabled = true;
            lastNameTextBox1.IsEnabled = true;
            firstNameTextBox1.Text = tempfirstNameTextBox1;
            lastNameTextBox1.Text = templastNameTextBox1;
            Keyboard.Focus(firstNameTextBox1);
        }

        private void btnEditI_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            string tempcolorTextBox = colorTextBox.Text.ToString();
            string tempmakeTextBox = makeTextBox.Text.ToString();
            btnNewI.IsEnabled = false;
            btnEditI.IsEnabled = false;
            btnDeleteI.IsEnabled = false;
            btnSaveI.IsEnabled = true;
            btnCancelI.IsEnabled = true;
            btnPrevI.IsEnabled = false;
            btnNextI.IsEnabled = false;
            colorTextBox.IsEnabled = true;
            makeTextBox.IsEnabled = true;
            colorTextBox.Text = tempcolorTextBox;
            makeTextBox.Text = tempmakeTextBox;
            Keyboard.Focus(colorTextBox);
        }

        private void btnEditO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            btnNewO.IsEnabled = false;
            btnEditO.IsEnabled = false;
            btnDeleteO.IsEnabled = false;
            btnSaveO.IsEnabled = true;
            btnCancelO.IsEnabled = true;
            btnPrevO.IsEnabled = false;
            btnNextO.IsEnabled = false;
        }

        private void btnDelete_Click_1(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempfirstNameTextBox1 = firstNameTextBox1.Text.ToString();
            string templastNameTextBox1 = lastNameTextBox1.Text.ToString();
            btnNew.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnSave.IsEnabled = true;
            btnCancel.IsEnabled = true;
            btnPrev.IsEnabled = false;
            btnNext.IsEnabled = false;
            firstNameTextBox1.Text = tempfirstNameTextBox1;
            lastNameTextBox1.Text = templastNameTextBox1;
        }

        private void btnDeleteI_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            string tempcolorTextBox = colorTextBox.Text.ToString();
            string tempmakeTextBox = makeTextBox.Text.ToString();
            btnNewI.IsEnabled = false;
            btnEditI.IsEnabled = false;
            btnDeleteI.IsEnabled = false;
            btnSaveI.IsEnabled = true;
            btnCancelI.IsEnabled = true;
            btnPrevI.IsEnabled = false;
            btnNextI.IsEnabled = false;
            colorTextBox.Text = tempcolorTextBox;
            makeTextBox.Text = tempmakeTextBox;
        }

        private void btnDeleteO_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
            btnNewO.IsEnabled = false;
            btnEditO.IsEnabled = false;
            btnDeleteO.IsEnabled = false;
            btnSaveO.IsEnabled = true;
            btnCancelO.IsEnabled = true;
            btnPrevO.IsEnabled = false;
            btnNextO.IsEnabled = false;
        }

        private void btnCancelO_Click_1(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNewO.IsEnabled = true;
            btnEditO.IsEnabled = true;
            btnDeleteO.IsEnabled = true;
            btnSaveO.IsEnabled = false;
            btnCancelO.IsEnabled = false;
            btnPrevO.IsEnabled = true;
            btnNextO.IsEnabled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNew.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnSave.IsEnabled = false;
            btnCancel.IsEnabled = false;
            btnPrev.IsEnabled = true;
            btnNext.IsEnabled = true;
            firstNameTextBox1.IsEnabled = false;
            lastNameTextBox1.IsEnabled = false;
        }

        private void btnCancelI_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Nothing;
            btnNewI.IsEnabled = true;
            btnEditI.IsEnabled = true;
            btnDeleteI.IsEnabled = true;
            btnSaveI.IsEnabled = false;
            btnCancelI.IsEnabled = false;
            btnPrevI.IsEnabled = true;
            btnNextI.IsEnabled = true;
            colorTextBox.IsEnabled = false;
            makeTextBox.IsEnabled = false;
        }


    }

    }
