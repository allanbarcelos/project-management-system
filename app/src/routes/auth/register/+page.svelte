<script lang="ts">
  import { goto } from "$app/navigation";
  import { authStore, setAuthState } from "$lib/stores/authStore";
  import { faL } from "@fortawesome/free-solid-svg-icons";
  import { onMount } from "svelte";
  import toast from "svelte-french-toast";

  let firstName = "";
  let lastName = "";
  let email = "";
  let password = "";
  let passwordConfirm = "";
  let isLoading = false;

  // Validation states
  let firstNameError = "";
  let lastNameError = "";
  let emailError = "";
  let passwordError = "";
  let confirmPasswordError = "";

  // Validation functions
  const validateFirstName = () => {
    if (!firstName) {
      firstNameError = "First name is required";
      return false;
    }
    if (firstName.length < 2 || firstName.length > 50) {
      firstNameError = "First name must be between 2 and 50 characters";
      return false;
    }
    firstNameError = "";
    return true;
  };

  const validateLastName = () => {
    if (!lastName) {
      lastNameError = "Last name is required";
      return false;
    }
    if (lastName.length < 2 || lastName.length > 50) {
      lastNameError = "Last name must be between 2 and 50 characters";
      return false;
    }
    lastNameError = "";
    return true;
  };

  const validateEmail = () => {
    if (!email) {
      emailError = "Email is required";
      return false;
    }
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) {
      emailError = "Please enter a valid email address";
      return false;
    }
    emailError = "";
    return true;
  };

  const validatePassword = () => {
    if (!password) {
      passwordError = "Password is required";
      return false;
    }
    if (!/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$/.test(password)) {
      passwordError = "Password must be at least 6 characters and contain uppercase, lowercase, and number";
      return false;
    }
    passwordError = "";
    return true;
  };

  const validatePasswordMatch = () => {
    if (!passwordConfirm) {
      confirmPasswordError = "Please confirm your password";
      return false;
    }
    if (password !== passwordConfirm) {
      confirmPasswordError = "Passwords do not match";
      return false;
    }
    confirmPasswordError = "";
    return true;
  };

  // Validate on input
  $: if (firstName) validateFirstName();
  $: if (lastName) validateLastName();
  $: if (email) validateEmail();
  $: if (password) validatePassword();
  $: if (passwordConfirm) validatePasswordMatch();

  const handleRegister = async () => {
    isLoading = true;

    // Validate all fields
    const isFirstNameValid = validateFirstName();
    const isLastNameValid = validateLastName();
    const isEmailValid = validateEmail();
    const isPasswordValid = validatePassword();
    const isPasswordMatchValid = validatePasswordMatch();

    // If any validation fails, stop submission
    if (!isFirstNameValid || !isLastNameValid || !isEmailValid || !isPasswordValid || !isPasswordMatchValid) {
      isLoading = false;
      return;
    }

    try {
      const response = await fetch(
        `${import.meta.env.VITE_API_URL}/auth/register`,
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify({ firstName, lastName, email, password }),
        }
      );

      if (response.ok) {
        toast.success("Registration successful! Redirecting to login...", { duration: 3000 });
        setTimeout(() => goto("/auth/login"), 3000);
      } else {
        const errorData = await response.json();
        if (Array.isArray(errorData)) {
          errorData.forEach((e: { code: string; description: string }) => {
            toast.error(e.description, { duration: 5000, position: "top-right" });
          });
        } else {
          toast.error("Registration failed. Please try again.", { duration: 5000 });
        }
      }
    } catch (error: any) {
      console.error("Registration error:", error);
      toast.error(error?.message || "An error occurred. Please try again later.", { duration: 5000 });
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
                class="form-control {firstNameError ? 'is-invalid' : ''}"
                id="inputFirstName"
                type="text"
                placeholder="Enter your first name"
                bind:value={firstName}
                on:blur={validateFirstName}
                required
              />
              <label for="inputFirstName">First name</label>
              {#if firstNameError}
                <div class="invalid-feedback">
                  {firstNameError}
                </div>
              {/if}
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-floating">
              <input
                class="form-control {lastNameError ? 'is-invalid' : ''}"
                id="inputLastName"
                type="text"
                placeholder="Enter your last name"
                bind:value={lastName}
                on:blur={validateLastName}
                required
              />
              <label for="inputLastName">Last name</label>
              {#if lastNameError}
                <div class="invalid-feedback">
                  {lastNameError}
                </div>
              {/if}
            </div>
          </div>
        </div>
        <div class="form-floating mb-3">
          <input
            class="form-control {emailError ? 'is-invalid' : ''}"
            id="inputEmail"
            type="email"
            placeholder="name@example.com"
            bind:value={email}
            on:blur={validateEmail}
            required
          />
          <label for="inputEmail">Email address</label>
          {#if emailError}
            <div class="invalid-feedback">
              {emailError}
            </div>
          {/if}
        </div>
        <div class="row mb-3">
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control {passwordError ? 'is-invalid' : ''}"
                id="inputPassword"
                type="password"
                placeholder="Create a password"
                bind:value={password}
                on:blur={validatePassword}
                on:input={() => {
                  validatePassword();
                  if (passwordConfirm) validatePasswordMatch();
                }}
                required
              />
              <label for="inputPassword">Password</label>
              {#if passwordError}
                <div class="invalid-feedback">
                  {passwordError}
                </div>
              {/if}
            </div>
          </div>
          <div class="col-md-6">
            <div class="form-floating mb-3 mb-md-0">
              <input
                class="form-control {confirmPasswordError ? 'is-invalid' : ''}"
                id="inputPasswordConfirm"
                type="password"
                placeholder="Confirm password"
                bind:value={passwordConfirm}
                on:blur={validatePasswordMatch}
                on:input={validatePasswordMatch}
                required
              />
              <label for="inputPasswordConfirm">Confirm Password</label>
              {#if confirmPasswordError}
                <div class="invalid-feedback">
                  {confirmPasswordError}
                </div>
              {/if}
            </div>
          </div>
        </div>
        <div class="mt-4 mb-0">
          <div class="d-grid">
            <button class="btn btn-primary btn-block" type="submit" disabled={isLoading}>
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
