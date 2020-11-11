// Copyright (c) 2015-present, Parse, LLC.  All rights reserved.  This source code is licensed under the BSD-style license found in the LICENSE file in the root directory of this source tree.  An additional grant of patent rights can be found in the PATENTS file in the same directory.

using Parse.Internal;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Parse
{
    /// <summary>
    /// Provides a set of utilities for using Parse with Apple.
    /// </summary>
    public static partial class ParseAppleUtils
    {
        private static readonly AppleAuthenticationProvider authProvider =
            new AppleAuthenticationProvider();

#if !UNITY
        /// <summary>
        /// Gets the Apple Application ID as supplied to <see cref="ParseAppleUtils.Initialize"/>
        /// </summary>
        public static string ApplicationId
        {
            get
            {
                return authProvider.AppId;
            }
        }
#endif

        /// <summary>
        /// Gets the access token for the currently logged in Apple user. This can be used with a
        /// Apple SDK to get access to Apple user data.
        /// </summary>
        public static string AccessToken
        {
            get
            {
                return authProvider.AccessToken;
            }
        }

#if !UNITY
        /// <summary>
        /// Initializes Apple for use with Parse.
        /// </summary>
        /// <param name="applicationId">Your Apple application ID.</param>
        public static void Initialize(string applicationId)
        {
            authProvider.AppId = applicationId;
            ParseUser.RegisterProvider(authProvider);
        }
#else
    /// <summary>
    /// Unity will just auto-initialize this. Since we're not responsible for login, we don't
    /// need the application id -- just the tokens.
    /// </summary>
    internal static void Initialize() {
      ParseUser.RegisterProvider(authProvider);
    }
#endif

        /// <summary>
        /// Logs in a <see cref="ParseUser" /> using Apple for authentication. If a user for the
        /// given Apple credentials does not already exist, a new user will be created.
        /// </summary>
        /// <param name="appleId">The user's Apple ID.</param>
        /// <param name="accessToken">A valid access token for the user.</param>
        /// <param name="expiration">The expiration date of the access token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The user that was either logged in or created.</returns>
        public static Task<ParseUser> logInWithAuthTypeAsync(string appleId,
            string accessToken,
            DateTime expiration,
            CancellationToken cancellationToken)
        {
            return ParseUser.LogInWithAsync("apple",
                authProvider.GetAuthData(appleId, accessToken, expiration),
                cancellationToken);
        }

        /// <summary>
        /// Logs in a <see cref="ParseUser" /> using Apple for authentication. If a user for the
        /// given Apple credentials does not already exist, a new user will be created.
        /// </summary>
        /// <param name="appleId">The user's Apple ID.</param>
        /// <param name="accessToken">A valid access token for the user.</param>
        /// <param name="expiration">The expiration date of the access token.</param>
        /// <returns>The user that was either logged in or created.</returns>
        public static Task<ParseUser> logInWithAuthTypeAsync(string appleId,
            string accessToken,
            DateTime expiration)
        {
            return logInWithAuthTypeAsync(appleId, accessToken, expiration, CancellationToken.None);
        }

        /// <summary>
        /// Links a <see cref="ParseUser" /> to a Apple account, allowing you to use Apple
        /// for authentication, and providing access to Apple data for the user.
        /// </summary>
        /// <param name="user">The user to link to a Apple account.</param>
        /// <param name="appleId">The user's Apple ID.</param>
        /// <param name="accessToken">A valid access token for the user.</param>
        /// <param name="expiration">The expiration date of the access token.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static Task LinkAsync(ParseUser user,
            string appleId,
            string accessToken,
            DateTime expiration,
            CancellationToken cancellationToken)
        {
            return user.LinkWithAsync("apple",
                authProvider.GetAuthData(appleId, accessToken, expiration),
                cancellationToken);
        }

        /// <summary>
        /// Links a <see cref="ParseUser" /> to a Apple account, allowing you to use Apple
        /// for authentication, and providing access to Apple data for the user.
        /// </summary>
        /// <param name="user">The user to link to a Apple account.</param>
        /// <param name="appleId">The user's Apple ID.</param>
        /// <param name="accessToken">A valid access token for the user.</param>
        /// <param name="expiration">The expiration date of the access token.</param>
        public static Task LinkAsync(ParseUser user,
            string appleId,
            string accessToken,
            DateTime expiration)
        {
            return LinkAsync(user, appleId, accessToken, expiration, CancellationToken.None);
        }

        /// <summary>
        /// Gets whether the given user is linked to a Apple account. This can only be used on
        /// the currently authorized user.
        /// </summary>
        /// <param name="user">The user to check.</param>
        /// <returns><c>true</c> if the user is linked to a Apple account.</returns>
        public static bool IsLinked(ParseUser user)
        {
            return user.IsLinked("apple");
        }

        /// <summary>
        /// Unlinks a user from a Apple account. Unlinking a user will save the user's data.
        /// </summary>
        /// <param name="user">The user to unlink.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public static Task UnlinkAsync(ParseUser user, CancellationToken cancellationToken)
        {
            return user.UnlinkFromAsync("apple", cancellationToken);
        }

        /// <summary>
        /// Unlinks a user from a Apple account. Unlinking a user will save the user's data.
        /// </summary>
        /// <param name="user">The user to unlink.</param>
        public static Task UnlinkAsync(ParseUser user)
        {
            return UnlinkAsync(user, CancellationToken.None);
        }
    }
}
