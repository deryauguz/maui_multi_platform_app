
﻿using Microsoft.Extensions.DependencyInjection;
namespace GorselProgramlamaOdev3;


    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            //return new Window(new AppShell());
            return new Window(new GirisSayfasi());
        }
    }

