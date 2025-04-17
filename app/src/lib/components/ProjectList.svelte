<script lang="ts">
    import { onMount } from 'svelte';
    import api from '../api';
    import type {Project}  from '../../Project';
  
    let projects: Project[] = [];
    let currentPage = 1;
    let totalPages = 1;
    const pageSize = 10;
  
    async function fetchProjects(page = 1) {
      const res = await api.get(`/projects?page=${page}&pageSize=${pageSize}`);
      projects = res.data.items;
      totalPages = res.data.totalPages;
      currentPage = page;
    }
  
    function nextPage() {
      if (currentPage < totalPages) fetchProjects(currentPage + 1);
    }
  
    function prevPage() {
      if (currentPage > 1) fetchProjects(currentPage - 1);
    }
  
    onMount(() => fetchProjects());
  </script>
  