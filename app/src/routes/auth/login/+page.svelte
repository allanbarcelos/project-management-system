<script lang="ts">
  import { goto, pushState } from "$app/navigation";
  import { authStore, setAuthState } from "$lib/stores/authStore";

    let email = '';
    let password = '';
    let errorMessage = '';

    const handleLogin = async () => {
        errorMessage = '';

        // Simple validation
        if (!email || !password) {
            errorMessage = 'Both email and password are required.';
            return;
        }

        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!emailRegex.test(email)) {
            errorMessage = 'Please enter a valid email address.';
            return;
        }

        try {
            const response = await fetch(`${import.meta.env.VITE_API_URL}/auth/login`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
            });

            if (response.ok) {
            const data = await response.json();
            const token = data.token;

            setAuthState({ isAuthenticated: true, user: { email }, token });

            goto('/');
            } else {
            const errorData = await response.json();
            errorMessage = errorData.message || 'Login failed, please try again.';
            }
        } catch (error) {
            errorMessage = 'An error occurred. Please try again later.';
            console.error('Login error:', error);
        }
    }

</script>

<div class="col-lg-5">
    <div class="card shadow-lg border-0 rounded-lg mt-5">
        <div class="card-header"><h3 class="text-center font-weight-light my-3">Login</h3></div>
        <div class="card-body">
            <form on:submit|preventDefault={handleLogin}>
                <div class="form-floating mb-3">
                    <input class="form-control" id="inputEmail" type="email" placeholder="name@example.com" bind:value={email} required/>
                    <label for="inputEmail">Email address</label>
                </div>
                <div class="form-floating mb-3">
                    <input class="form-control" id="inputPassword" type="password" placeholder="Password" bind:value={password} required/>
                    <label for="inputPassword">Password</label>
                </div>
                {#if errorMessage }
                <div class="alert alert-danger" role="alert">
                    {errorMessage}
                </div>
                {/if}
                <div class="d-flex align-items-center justify-content-between mt-4 mb-0">
                    <a class="small" href="/auth/forgot-password">Forgot Password?</a>
                    <button class="btn btn-primary" type="submit">Login</button>
                </div>
            </form>
        </div>
        <div class="card-footer text-center py-3">
            <div class="small"><a href="/auth/register">Need an account? Sign up!</a></div>
        </div>
    </div>
</div>