// See https://kit.svelte.dev/docs/types#app
// for information about these interfaces

// and what to do when importing types
declare namespace App {
	// interface Error {}
	// interface Locals {}
	// interface PageData {}
	// interface Platform {}
}

declare module '*.svg?component' {
    const content: ConstructorOfATypedSvelteComponent
    export default content
}

declare module '*.svg?src' {
    const content: string
    export default content
}

declare module '*.svg?url' {
    const content: string
    export default content
}

declare module '*.svg?dataurl' {
    const content: string
    export default content
}