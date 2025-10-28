using Domain.Gameplay.Models;
using Infrastructure;
using Presentation.Gameplay.Presenters;
using Presentation.Gameplay.Views;
using Repositories.Gameplay;
using UnityEngine;
using UseCases.Gameplay;
using VContainer;
using VContainer.Unity;

namespace Installers.Gameplay
{
    public class GameplayInstaller : LifetimeScope
    {
        public GridRepository GridRepository;
        public BuildingsRepository BuildingsRepository;
        public GridView GridView;
        public BuildingGhostView BuildingGhostView;
        public Camera Camera;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(GridRepository);
            builder.RegisterInstance(BuildingsRepository);
            
            builder.RegisterInstance(GridView);
            builder.RegisterInstance(BuildingGhostView);
            
            builder.RegisterInstance(Camera);
            
            builder.RegisterEntryPoint<RaycastFromCameraService>();
          
            builder.Register<CreateGridUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ChangeSelectedCellUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ChangeSelectedBuildingTypeUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
           
            builder.Register<GridPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<BuildingGhostPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            
            builder.Register<GridModel>(Lifetime.Singleton);
            builder.Register<BuildingProcessModel>(Lifetime.Singleton);
        }
    }
}