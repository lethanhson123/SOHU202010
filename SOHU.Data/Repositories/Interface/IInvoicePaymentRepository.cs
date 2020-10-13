﻿using SOHU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SOHU.Data.Repositories
{
    public interface IInvoicePaymentRepository : IRepository<InvoicePayment>
    {
        public List<InvoicePayment> GetByInvoiceIDToList(int invoiceID);
    }
}
