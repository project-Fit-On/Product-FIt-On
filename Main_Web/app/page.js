import styles from "./page.module.css";
import Link from "next/link";
import Image from "next/image";

export default function Home() {
  return (
    <div className={styles.container}>
      <section className={styles.bannerSection}>
        <div className={styles.bannerContent}>
          <h1 className={styles.title}>Try Before You Buy</h1>
          <h2 className={styles.subtitle}>The Smart Way to Shop</h2>
          <p className={styles.description}>
            Personalized Fashion at Your Fingertips
          </p>

          <div className={styles.buttonContainer}>
            <Link href="/catalog" className={styles.primaryButton}>
              Shop Now
            </Link>
            <Link href="/how-it-works" className={styles.secondaryButton}>
              Learn More
            </Link>
          </div>
        </div>

        <div className={styles.bannerImage}>
          {/* You can optionally use Image component instead of CSS background */}
          {/* <Image 
            src="/fashion-illustration.png"
            alt="Fashion Illustration"
            fill
            style={{ objectFit: 'contain' }}
            priority
          /> */}
        </div>
      </section>
    </div>
  );
}
