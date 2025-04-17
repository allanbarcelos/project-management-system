<script lang="ts">
  import { goto } from "$app/navigation";
  import { authStore, setAuthState } from "$lib/stores/authStore";
  import toast from "svelte-french-toast";

  let firstName = "";
  let lastName = "";
  let email = "";
  let password = "";
  let passwordConfirm = "";
  let errorMessage = "";

  let isLoading = false;

  const handleRegister = async () => {
    errorMessage = '';
    isLoading = true;

    // Basic frontend validation
    if (!firstName || !lastName || !email || !password || !passwordConfirm) {
      errorMessage = 'All fields are required.';
      toast.error(errorMessage, { duration: 4000 });
      isLoading = false;
      return;
    }

    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
      errorMessage = 'Please enter a valid email address.';
      toast.error(errorMessage, { duration: 4000 });
      isLoading = false;
      return;
    }

    if (password.length < 6) {
      errorMessage = 'Password must be at least 6 characters long.';
      toast.error(errorMessage, { duration: 4000 });
      isLoading = false;
      return;
    }

    if (password !== passwordConfirm) {
      errorMessage = 'Passwords do not match.';
      toast.error(errorMessage, { duration: 4000 });
      isLoading = false;
      return;
    }

    try {
      const response = await fetch('http://localhost:5086/auth/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ firstName, lastName, email, password })
    });

      if (response.ok) {
        toast.success('Registration successful!', { duration: 3000 });
        setTimeout(() => goto('/auth/login'), 3000);
      } else {
        const errorData = await response.json();
        if (Array.isArray(errorData)) {
          errorData.forEach((e: { code: string; description: string }) => {
            toast.error(e.description, { duration: 5000, position: 'top-right' });
          });
        } else {
          toast.error('Unexpected error occurred. Please try again later.', { duration: 5000 });
        }
      }
    } catch (error: unknown) {
      const err = error as Error;
      errorMessage = 'An error occurred. Please try again later.';
      console.error('Register error:', err);
      toast.error(err.message || errorMessage, { duration: 5000 });
    } finally {
      isLoading = false;
    }
  };
</script>

<div class="col-lg-7">
  <div class="card shadow-lg border-0 rounded-lg mt-5">
    <div class="card-header">
      <h3 class="text-center font-weight-light my-3">Create Account</h3>
    </div>
    <div class="card-body">
      <form on:submit|preventDefault={handleRegister}>
        <div class="row mb-3">
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control"
                id="inputFirstName"
                type="text"
                placeholder="Enter your first name"
                bind:value={firstName}
              />
              <label for="inputFirstName">First name</label>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-floating">
              <input
                class="form-control"
                id="inputLastName"
                type="text"
                placeholder="Enter your last name"
                bind:value={lastName}
              />
              <label for="inputLastName">Last name</label>
            </div>
          </div>
        </div>
        <div class="form-floating mb-3">
          <input
            class="form-control"
            id="inputEmail"
            type="email"
            placeholder="name@example.com"
            bind:value={email}
            required
          />
          <label for="inputEmail">Email address</label>
        </div>
        <div class="row mb-3">
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control"
                id="inputPassword"
                type="password"
                placeholder="Create a password"
                bind:value={password}
                required
              />
              <label for="inputPassword">Password</label>
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control"
                id="inputPasswordConfirm"
                type="password"
                placeholder="Confirm password"
                bind:value={passwordConfirm}
                required
              />
              <label for="inputPasswordConfirm">Confirm Password</label>
            </div>
          </div>
        </div>
        <div class="mt-4 mb-0">
          <div class="d-grid">
            <button class="btn btn-primary btn-block" type="submit">
              {#if isLoading}
              <div class="spinner-border spinner-border-sm" role="status">
                <span class="visually-hidden">Loading...</span>
              </div>
              {/if}
              Create Account
            </button>
          </div>
        </div>
      </form>
    </div>
    <div class="card-footer text-center py-3">
      <div class="small">
        <a href="/auth/login">Have an account? Go to login</a>
      </div>
    </div>
  </div>
</div>
