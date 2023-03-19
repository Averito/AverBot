import type {NextPage} from 'next'
import dynamic from "next/dynamic";

const ClientComponent = dynamic(() => import('./ClientComponent'), { ssr: false })
export const Home: NextPage = () => {
    return (
        <>
            <ClientComponent />
        </>
    )
}
