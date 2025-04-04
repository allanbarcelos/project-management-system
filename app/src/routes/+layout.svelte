<script>
  import MainLayout from "$lib/layouts/MainLayout.svelte";

  import { get } from "svelte/store";
  import { authStore } from "$lib/stores/authStore";

  import "./../scss/styles.scss";
  import "./../../node_modules/@fortawesome/fontawesome-free/scss/fontawesome.scss";
  import { onMount } from "svelte";

  import { Toaster } from 'svelte-french-toast';

  const isAuthenticated = get(authStore).isAuthenticated;

  onMount(() => {
    if (isAuthenticated) {
      document.bodyD.classList.add("sb-nav-fixed");
      return () => {
        document.body.classList.remove("sb-nav-fixed");
      };
    }
    return () => { };
  });
</script>

<Toaster />

{#if isAuthenticated}
  <MainLayout>
    <slot />
  </MainLayout>
{:else}
  <slot />
{/if}
