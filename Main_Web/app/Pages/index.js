import Head from "next/head";
import Link from "next/link";
import styles from "../styles/Home.module.css";
// Fix the import path - assuming HowItWorks is in the same directory
import HowItWorks from "./HowItWorks";

export default function Home() {
  return (
    <div className={styles.container}>
      <Head>
        <title>Try Before You Buy – The Smart Way to Shop</title>
        <meta
          name="description"
          content="Personalized Fashion at Your Fingertips"
        />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main>
        <section className={styles.bannerSection}>
          <div className={styles.bannerContent}>
            <h1 className={styles.title}>Try Before You Buy</h1>
            <h2 className={styles.subtitle}>The Smart Way to Shop</h2>
            <p className={styles.description}>
              Personalized Fashion at Your Fingertips
            </p>

            <div className={styles.buttonContainer}>
              <Link href="/catalog">
                <button className={styles.primaryButton}>Shop Now</button>
              </Link>
              <Link href="/how-it-works">
                <button className={styles.secondaryButton}>Learn More</button>
              </Link>
            </div>
          </div>

          <div className={styles.bannerImage}>
            {/* This div will be styled with the fashion illustration */}
          </div>
        </section>
        <HowItWorks />
      </main>
    </div>
  );
}
