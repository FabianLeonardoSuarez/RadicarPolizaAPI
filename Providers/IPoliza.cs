using System;
using System.Collections.Generic;
using RadicarPolizaAPI.Models;

namespace RadicarPolizaAPI.Data;
    public interface IPoliza
    {
        public Poliza GetPolizaByPlacaOrIdPoliza(string SearchKey);
        public bool CreatePoliza(Poliza newpoliza);
    }