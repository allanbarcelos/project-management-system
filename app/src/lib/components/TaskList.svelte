<script>
    import { onMount } from 'svelte';
    import api from '../api.js';
  
    let tasks: Tasks[] = [];
    let currentPage = 1;
    let totalPages = 1;
    const pageSize = 10;
  
    async function fetchTasks(page = 1) {
      const res = await api.get(`/tasks?page=${page}&pageSize=${pageSize}`);
      tasks = res.data.items;
      totalPages = res.data.totalPages;
      currentPage = page;
    }
  
    function nextPage() {
      if (currentPage < totalPages) fetchTasks(currentPage + 1);
    }
  
    function prevPage() {
      if (currentPage > 1) fetchTasks(currentPage - 1);
    }
  
    onMount(() => fetchTasks());
  </script>
  
  <h2 class="text-xl font-bold mb-4">Tasks</h2>
  
  <ul>
    {#each tasks as task}
      <li class="mb-2 border p-2 rounded">{task.title}</li>
    {/each}
  </ul>
  
  <div class="mt-4 flex gap-2 items-center">
    <button on:click={prevPage} disabled={currentPage === 1}>Previous</button>
    <span>Page {currentPage} of {totalPages}</span>
    <button on:click={nextPage} disabled={currentPage === totalPages}>Next</button>
  </div>
  