using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSLNG.PEAR.Data.Entities;
using DSLNG.PEAR.Data.Persistence;

namespace DSLNG.PEAR.Data.Installer
{
    public class GroupsInstaller
    {
        private readonly DataContext _dataContext;
        public GroupsInstaller(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Installer()
        {
            ////var group = new Group();
            ////group.Id = 1;
            ////group.IsActive = true;
            ////group.Name = "Fatality";
            ////group.Order = 1;
            ////group.Remark = "test";

            ////var group2 = new Group();
            ////group2.Id = 2;
            ////group2.IsActive = true;
            ////group2.Name = "Security";
            ////group2.Order = 2;
            ////group2.Remark = "test";


            ////var gr1 = new Group { Id = 1, Name = "Fatality", Order=1, IsActive=1 };

            //_dataContext.Groups.AddOrUpdate(group);
            //_dataContext.Groups.AddOrUpdate(group2);
            var gr1 = new Group { Id = 1, Name = "Fatality", Order = 1, IsActive = true };
            var gr2 = new Group { Id = 2, Name = "DAFWC", Order = 1, IsActive = true };
            var gr3 = new Group { Id = 3, Name = "LOPC", Order = 1, IsActive = true };
            var gr4 = new Group { Id = 4, Name = "Security", Order = 1, IsActive = true };
            var gr5 = new Group { Id = 5, Name = "LNG Production", Order = 1, IsActive = true };
            var gr6 = new Group { Id = 6, Name = "Flarred Gas", Order = 1, IsActive = true };
            var gr7 = new Group { Id = 7, Name = "Thermal Efficiency", Order = 1, IsActive = true };
            var gr8 = new Group { Id = 8, Name = "Plant Availibility", Order = 1, IsActive = true };
            var gr9 = new Group { Id = 9, Name = "Plant Reliability", Order = 1, IsActive = true };
            var gr10 = new Group { Id = 10, Name = "Cargo Commitment", Order = 1, IsActive = true };
            var gr11 = new Group { Id = 11, Name = "Dividen Payout Ratio", Order = 1, IsActive = true };
            var gr12 = new Group { Id = 12, Name = "Debt Service Ratio", Order = 1, IsActive = true };
            var gr13 = new Group { Id = 13, Name = "Ebitda", Order = 1, IsActive = true };
            var gr14 = new Group { Id = 14, Name = "Budget Utilization", Order = 1, IsActive = true };
            var gr15 = new Group { Id = 15, Name = "Total Operating Cost", Order = 1, IsActive = true };
            var gr16 = new Group { Id = 16, Name = "Production Cost", Order = 1, IsActive = true };
            var gr17 = new Group { Id = 17, Name = "Positive Tone", Order = 1, IsActive = true };
            var gr18 = new Group { Id = 18, Name = "Program recognized by Gov & Institution", Order = 1, IsActive = true };
            var gr19 = new Group { Id = 19, Name = "Community acceptance through CSR program & CR", Order = 1, IsActive = true };
            var gr20 = new Group { Id = 20, Name = "Social ROI", Order = 1, IsActive = true };
            var gr21 = new Group { Id = 21, Name = "EBITDA/Employee", Order = 1, IsActive = true };
            var gr22 = new Group { Id = 22, Name = "Cost/POB ", Order = 1, IsActive = true };
            var gr23 = new Group { Id = 23, Name = "Employee Retention", Order = 1, IsActive = true };
            var gr24 = new Group { Id = 24, Name = "Employee Turn over", Order = 1, IsActive = true };
            var gr25 = new Group { Id = 25, Name = "ICT service availability", Order = 1, IsActive = true };
            var gr26 = new Group { Id = 26, Name = "QHSE training attend", Order = 1, IsActive = true };
            var gr27 = new Group { Id = 27, Name = "QHSE Closure actions", Order = 1, IsActive = true };
            var gr28 = new Group { Id = 28, Name = "% fitness for work", Order = 1, IsActive = true };
            var gr29 = new Group { Id = 29, Name = "PS-Near miss", Order = 1, IsActive = true };
            var gr30 = new Group { Id = 30, Name = "Traffic transportation incidents", Order = 1, IsActive = true };
            var gr31 = new Group { Id = 31, Name = "Activation of a safety inspection system", Order = 1, IsActive = true };
            var gr32 = new Group { Id = 32, Name = "Activation of a Mechanical shutdown system", Order = 1, IsActive = true };
            var gr33 = new Group { Id = 33, Name = "Activation of pressure relief device", Order = 1, IsActive = true };
            var gr34 = new Group { Id = 34, Name = "alarm rate/operator", Order = 1, IsActive = true };
            var gr35 = new Group { Id = 35, Name = "standing alarm", Order = 1, IsActive = true };
            var gr36 = new Group { Id = 36, Name = "# backlog in corrective & preventive plant maintenance", Order = 1, IsActive = true };
            var gr37 = new Group { Id = 37, Name = "Planned hour vs actual hours", Order = 1, IsActive = true };
            var gr38 = new Group { Id = 38, Name = "Planned Shutdown (hours)", Order = 1, IsActive = true };
            var gr39 = new Group { Id = 39, Name = "Unplanned shutdown (hours)", Order = 1, IsActive = true };
            var gr40 = new Group { Id = 40, Name = "Maintenance efficiency (budget)", Order = 1, IsActive = true };
            var gr41 = new Group { Id = 41, Name = "Recordable Injury Frequency", Order = 1, IsActive = true };
            var gr42 = new Group { Id = 42, Name = "Feed Gas Supply", Order = 1, IsActive = true };
            var gr43 = new Group { Id = 43, Name = "Feed Gas Supply(Senoro)", Order = 1, IsActive = true };
            var gr44 = new Group { Id = 44, Name = "Feed Gas Supply(Matindok)", Order = 1, IsActive = true };
            var gr45 = new Group { Id = 45, Name = "Condensate Production", Order = 1, IsActive = true };
            var gr46 = new Group { Id = 46, Name = "Condensate Sales", Order = 1, IsActive = true };
            var gr47 = new Group { Id = 47, Name = "JCC Price (USD/bbl)", Order = 1, IsActive = true };
            var gr48 = new Group { Id = 48, Name = "Senoro Feed Gas Price (USD/mmbtu)", Order = 1, IsActive = true };
            var gr49 = new Group { Id = 49, Name = "Matindok Feed Gas Price (USD/mmbtu)", Order = 1, IsActive = true };
            var gr50 = new Group { Id = 50, Name = "Chubu SPA LNG Price (USD/mmbtu)", Order = 1, IsActive = true };
            var gr51 = new Group { Id = 51, Name = "Kyushu SPA LNG Price (USD/mmbtu)", Order = 1, IsActive = true };
            var gr52 = new Group { Id = 52, Name = "KOGAS SPA LNG Price (USD/mmbtu)", Order = 1, IsActive = true };
            var gr53 = new Group { Id = 53, Name = "LNG Spot Price (USD/mmbtu)", Order = 1, IsActive = true };
            var gr54 = new Group { Id = 54, Name = "Condensate Price (USD/bbl)", Order = 1, IsActive = true };
            var gr55 = new Group { Id = 55, Name = "Total Revenue (USD Million)", Order = 1, IsActive = true };
            var gr56 = new Group { Id = 56, Name = "Revenue from LNG Sales (USD Million)", Order = 1, IsActive = true };
            var gr57 = new Group { Id = 57, Name = "Revenue from Condensate Sales (USD Million)", Order = 1, IsActive = true };
            var gr58 = new Group { Id = 58, Name = "Total COGS - (USD Million)", Order = 1, IsActive = true };
            var gr59 = new Group { Id = 59, Name = "Total Cost of Feed Gas (USD Million)", Order = 1, IsActive = true };
            var gr60 = new Group { Id = 60, Name = "Direct Labor Cost  (USD Million)", Order = 1, IsActive = true };
            var gr61 = new Group { Id = 61, Name = "Factory Overhead (USD Million)", Order = 1, IsActive = true };
            var gr62 = new Group { Id = 62, Name = "Depreciation Expense (USD Million)", Order = 1, IsActive = true };
            var gr63 = new Group { Id = 63, Name = "OPEX - Shipping & Marketing (USD Million)", Order = 1, IsActive = true };
            var gr64 = new Group { Id = 64, Name = "OPEX - General & Administrative (USD Million)", Order = 1, IsActive = true };
            var gr65 = new Group { Id = 65, Name = "Income Tax (USD Million)", Order = 1, IsActive = true };
            var gr66 = new Group { Id = 66, Name = "Profit After Tax (USD Million)", Order = 1, IsActive = true };
            var gr67 = new Group { Id = 67, Name = "Project Cash Flow", Order = 1, IsActive = true };
            var gr68 = new Group { Id = 68, Name = "CAPEX", Order = 1, IsActive = true };
            var gr69 = new Group { Id = 69, Name = "Change in Working Capital", Order = 1, IsActive = true };
            var gr70 = new Group { Id = 70, Name = "Equity Cash Flow", Order = 1, IsActive = true };
            var gr71 = new Group { Id = 71, Name = "Equity Injections", Order = 1, IsActive = true };
            var gr72 = new Group { Id = 72, Name = "SH Loan Interest Return", Order = 1, IsActive = true };
            var gr73 = new Group { Id = 73, Name = "Cash Generated for Equity Holders", Order = 1, IsActive = true };
            var gr74 = new Group { Id = 74, Name = "Cash Flow Available for Debt Service/CFADS", Order = 1, IsActive = true };
            var gr75 = new Group { Id = 75, Name = "Debt Service", Order = 1, IsActive = true };
            var gr76 = new Group { Id = 76, Name = "NPV of CFADS", Order = 1, IsActive = true };
            var gr77 = new Group { Id = 77, Name = "Debt Balance", Order = 1, IsActive = true };
            var gr78 = new Group { Id = 78, Name = "Dividend Paid", Order = 1, IsActive = true };
            var gr79 = new Group { Id = 79, Name = "Retained Earnings for Dividend", Order = 1, IsActive = true };
            var gr80 = new Group { Id = 80, Name = "Loan Life Coverage Ratio", Order = 1, IsActive = true };

            _dataContext.Groups.AddOrUpdate(gr1);
            _dataContext.Groups.AddOrUpdate(gr2);
            _dataContext.Groups.AddOrUpdate(gr3);
            _dataContext.Groups.AddOrUpdate(gr4);
            _dataContext.Groups.AddOrUpdate(gr5);
            _dataContext.Groups.AddOrUpdate(gr6);
            _dataContext.Groups.AddOrUpdate(gr7);
            _dataContext.Groups.AddOrUpdate(gr8);
            _dataContext.Groups.AddOrUpdate(gr9);
            _dataContext.Groups.AddOrUpdate(gr10);
            _dataContext.Groups.AddOrUpdate(gr11);
            _dataContext.Groups.AddOrUpdate(gr12);
            _dataContext.Groups.AddOrUpdate(gr13);
            _dataContext.Groups.AddOrUpdate(gr14);
            _dataContext.Groups.AddOrUpdate(gr15);
            _dataContext.Groups.AddOrUpdate(gr16);
            _dataContext.Groups.AddOrUpdate(gr17);
            _dataContext.Groups.AddOrUpdate(gr18);
            _dataContext.Groups.AddOrUpdate(gr19);
            _dataContext.Groups.AddOrUpdate(gr20);
            _dataContext.Groups.AddOrUpdate(gr21);
            _dataContext.Groups.AddOrUpdate(gr22);
            _dataContext.Groups.AddOrUpdate(gr23);
            _dataContext.Groups.AddOrUpdate(gr24);
            _dataContext.Groups.AddOrUpdate(gr25);
            _dataContext.Groups.AddOrUpdate(gr26);
            _dataContext.Groups.AddOrUpdate(gr27);
            _dataContext.Groups.AddOrUpdate(gr28);
            _dataContext.Groups.AddOrUpdate(gr29);
            _dataContext.Groups.AddOrUpdate(gr30);
            _dataContext.Groups.AddOrUpdate(gr31);
            _dataContext.Groups.AddOrUpdate(gr32);
            _dataContext.Groups.AddOrUpdate(gr33);
            _dataContext.Groups.AddOrUpdate(gr34);
            _dataContext.Groups.AddOrUpdate(gr35);
            _dataContext.Groups.AddOrUpdate(gr36);
            _dataContext.Groups.AddOrUpdate(gr37);
            _dataContext.Groups.AddOrUpdate(gr38);
            _dataContext.Groups.AddOrUpdate(gr39);
            _dataContext.Groups.AddOrUpdate(gr40);
            _dataContext.Groups.AddOrUpdate(gr41);
            _dataContext.Groups.AddOrUpdate(gr42);
            _dataContext.Groups.AddOrUpdate(gr43);
            _dataContext.Groups.AddOrUpdate(gr44);
            _dataContext.Groups.AddOrUpdate(gr45);
            _dataContext.Groups.AddOrUpdate(gr46);
            _dataContext.Groups.AddOrUpdate(gr47);
            _dataContext.Groups.AddOrUpdate(gr48);
            _dataContext.Groups.AddOrUpdate(gr49);
            _dataContext.Groups.AddOrUpdate(gr50);
            _dataContext.Groups.AddOrUpdate(gr51);
            _dataContext.Groups.AddOrUpdate(gr52);
            _dataContext.Groups.AddOrUpdate(gr53);
            _dataContext.Groups.AddOrUpdate(gr54);
            _dataContext.Groups.AddOrUpdate(gr55);
            _dataContext.Groups.AddOrUpdate(gr56);
            _dataContext.Groups.AddOrUpdate(gr57);
            _dataContext.Groups.AddOrUpdate(gr58);
            _dataContext.Groups.AddOrUpdate(gr59);
            _dataContext.Groups.AddOrUpdate(gr60);
            _dataContext.Groups.AddOrUpdate(gr61);
            _dataContext.Groups.AddOrUpdate(gr62);
            _dataContext.Groups.AddOrUpdate(gr63);
            _dataContext.Groups.AddOrUpdate(gr64);
            _dataContext.Groups.AddOrUpdate(gr65);
            _dataContext.Groups.AddOrUpdate(gr66);
            _dataContext.Groups.AddOrUpdate(gr67);
            _dataContext.Groups.AddOrUpdate(gr68);
            _dataContext.Groups.AddOrUpdate(gr69);
            _dataContext.Groups.AddOrUpdate(gr70);
            _dataContext.Groups.AddOrUpdate(gr71);
            _dataContext.Groups.AddOrUpdate(gr72);
            _dataContext.Groups.AddOrUpdate(gr73);
            _dataContext.Groups.AddOrUpdate(gr74);
            _dataContext.Groups.AddOrUpdate(gr75);
            _dataContext.Groups.AddOrUpdate(gr76);
            _dataContext.Groups.AddOrUpdate(gr77);
            _dataContext.Groups.AddOrUpdate(gr78);
            _dataContext.Groups.AddOrUpdate(gr79);
            _dataContext.Groups.AddOrUpdate(gr80);


        }
    }
}
