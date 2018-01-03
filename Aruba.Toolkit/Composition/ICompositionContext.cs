//-----------------------------------------------------------------------
// <copyright file="ICompositionContext.cs" company="MyToolkit">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/MyToolkit/MyToolkit/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Aruba.Toolkit.Composition
{
    /// <summary>Provides the ability to store, retrieve and assemble parts. </summary>
    public interface ICompositionContext
    {
        /// <summary>Adds parts from a given assembly. </summary>
        /// <param name="assembly">The assembly. </param>
        /// <returns>The number of added parts. </returns>
        int AddPartsFromAssembly(Assembly assembly);

        /// <summary>Adds an existing part for a given interface and implementation type. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <typeparam name="TImplementation">The instance type of the part. </typeparam>
        /// <param name="part">The part. </param>
        /// <returns><c>true</c> if the part has been added. </returns>
        bool AddPart<TInterface, TImplementation>(TImplementation part);

        /// <summary>Adds an existing part for a given interface, implementation type and name. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <typeparam name="TImplementation">The instance type of the part. </typeparam>
        /// <param name="name">The name of the part. </param>
        /// <param name="part">The part. </param>
        /// <returns><c>true</c> if the part has been added. </returns>
        bool AddPart<TInterface, TImplementation>(TImplementation part, string name);

        /// <summary>Adds a part for a given interface and implementation type which is instantiated when first requested. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <typeparam name="TImplementation">The instance type of the part. </typeparam>
        /// <returns><c>true</c> if the part has been added. </returns>
        bool AddPart<TInterface, TImplementation>();

        /// <summary>Adds a part for a given interface, implementation type and name which is instantiated when first requested. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <typeparam name="TImplementation">The instance type of the part. </typeparam>
        /// <param name="name">The name of the part. </param>
        /// <returns><c>true</c> if the part has been added. </returns>
        bool AddPart<TInterface, TImplementation>(string name);

        /// <summary>Adds a part for a given interface and implementation type which is instantiated for each request. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <typeparam name="TImplementation">The instance type of the part. </typeparam>
        /// <returns><c>true</c> if the part has been added. </returns>
        bool AddPerRequestPart<TInterface, TImplementation>();

        /// <summary>Adds a part for a given interface, implementation type and name which is instantiated for each request. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <typeparam name="TImplementation">The instance type of the part. </typeparam>
        /// <param name="name">The name of the part. </param>
        /// <returns><c>true</c> if the part has been added. </returns>
        bool AddPerRequestPart<TInterface, TImplementation>(string name);

        /// <summary>Removes a part. </summary>
        /// <typeparam name="TInterface">The type of the part. </typeparam>
        /// <returns><c>true</c> if the part has been found and removed; otherwise, <c>false</c>. </returns>
        bool RemovePart<TInterface>();

        /// <summary>Removes a part. </summary>
        /// <typeparam name="TInterface">The type of the part. </typeparam>
        /// <param name="name">The name of the part. </param>
        /// <returns><c>true</c> if the part has been found and removed; otherwise, <c>false</c>. </returns>
        bool RemovePart<TInterface>(string name);

        /// <summary>Gets a single part. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <exception cref="InvalidOperationException">Multiple parts found. </exception>
        /// <returns>The part. </returns>
        TInterface GetPart<TInterface>();

        /// <summary>Gets a single part. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <param name="name">The name of the part. </param>
        /// <exception cref="InvalidOperationException">Multiple parts found. </exception>
        /// <returns>The part. </returns>
        TInterface GetPart<TInterface>(string name);

        /// <summary>Gets multiple parts of a given interface type. </summary>
        /// <typeparam name="TInterface">The interface type of the part. </typeparam>
        /// <returns>The list of parts. </returns>
        IList<TInterface> GetParts<TInterface>();

        /// <summary>Resolves all property injection annotations for the given object. </summary>
        /// <param name="obj">The object. </param>
        void SatisfyImports<T>(T obj);

        /// <summary>Builds the resolving cache for the given type. </summary>
        void BuildCache<T>();

        /// <summary>Removes all parts. </summary>
        void Clear();
    }
}