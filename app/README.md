# sv

Everything you need to build a Svelte project, powered by [`sv`](https://github.com/sveltejs/cli).

## Creating a project

If you're seeing this, you've probably already done this step. Congrats!

```bash
# create a new project in the current directory
npx sv create

# create a new project in my-app
npx sv create my-app
```

## Developing

Once you've created a project and installed dependencies with `npm install` (or `pnpm install` or `yarn`), start a development server:

```bash
npm run dev

# or start the server and open the app in a new browser tab
npm run dev -- --open
```

## Building

To create a production version of your app:

```bash
npm run build
```

You can preview the production build with `npm run preview`.

> To deploy your app, you may need to install an [adapter](https://svelte.dev/docs/kit/adapters) for your target environment.

<!-- Deploying the Application -->

## 1. Deploy to Vercel

[Vercel](https://vercel.com/) is a cloud platform for static sites and serverless functions, optimized for frameworks like Svelte. To deploy your Svelte app to Vercel:

### Steps:
1. If you don't already have a Vercel account, create one at [vercel.com](https://vercel.com/).
2. Install the Vercel CLI globally:

   ```bash
   npm install -g vercel

## 2. Deploy to Github Pages

1. Ensure your project is hosted on GitHub. If it's not already, create a new GitHub repository and push your project.

2. Install the gh-pages package:

    ```bash
    npm install --save-dev gh-pages
    ```

3. Add a deploy script to your package.json file:

 
    {
        "scripts": {
            "deploy": "gh-pages -d dist"
        }
    }

4. 
```bash
npm run build
```
5. 
```bash
npm run deploy
```

# This will push the dist/ folder to the gh-pages branch of your repository.



