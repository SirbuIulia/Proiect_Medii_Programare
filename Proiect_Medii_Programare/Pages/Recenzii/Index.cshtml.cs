﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect_Medii_Programare.Data;
using Proiect_Medii_Programare.Models;


namespace Proiect_Medii_Programare.Pages.Recenzii
{
    public class IndexModel : PageModel
    {
        private readonly Proiect_Medii_Programare.Data.Proiect_Medii_ProgramareContext _context;

        public IndexModel(Proiect_Medii_Programare.Data.Proiect_Medii_ProgramareContext context)
        {
            _context = context;
        }
        // public Restaurant SelectedRestaurant { get; set; }
        public IList<Recenzie> Recenzie { get; set; } = default!;

        public int RecenzieID
        {
            get; set;
        }
        public int RestaurantID { get; set; }

        public string NumeSort { get; set; }
        public string DataSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NumeSort = String.IsNullOrEmpty(sortOrder) ? "nume_asc" : "";

            DataSort = sortOrder == "data_desc" ? "data_desc" : "";
           

            Recenzie = await _context.Recenzie
                     .Include(b => b.Restaurant)
                     .Include(b => b.Client)
                     .ToListAsync();

            switch (sortOrder)
            {
                case "nume_asc":
                    Recenzie = Recenzie.OrderBy(s =>
                   s.Restaurant.Nume).ToList();
                    break;
                case "data_desc":
                    Recenzie = Recenzie.OrderByDescending(s =>
                   s.Data).ToList();
                    break;
                default:
                    Recenzie = Recenzie.OrderByDescending(s => s.Data).ToList();
                    break;

            }
        }
    }
}

