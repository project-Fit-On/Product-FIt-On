import React from "react";
import styles from "./Feature.module.css";
import { MdOutlineTimer, MdOutlineGpsFixed } from "react-icons/md";
import { FaShoppingBag, FaTshirt } from "react-icons/fa";

const WhyChooseFitOn = () => {
  const benefits = [
    {
      icon: <MdOutlineTimer />,
      title: "Save Time",
      description: "No more hours wasted on trying clothes.",
    },
    {
      icon: <MdOutlineGpsFixed />,
      title: "Perfect Fit",
      description: "Get accurate sizing and outfit suggestions.",
    },
    {
      icon: <FaShoppingBag />,
      title: "Seamless Shopping",
      description: "Shop confidently with virtual try-ons.",
    },
    {
      icon: <FaTshirt />,
      title: "Organized Wardrobe",
      description: "Manage your clothes effortlessly.",
    },
  ];

  return (
    <section className={styles.whyChooseSection}>
      <div className={styles.container}>
        <h2 className={styles.sectionTitle}>Why Choose Fit-On</h2>

        <div className={styles.benefitsContainer}>
          {benefits.map((benefit, index) => (
            <div
              key={index}
              className={styles.benefitCard}
              style={{ animationDelay: `${index * 0.15}s` }}
            >
              <div className={styles.iconWrapper}>
                <span className={styles.icon}>{benefit.icon}</span>
              </div>
              <h3 className={styles.benefitTitle}>{benefit.title}</h3>
              <p className={styles.benefitDescription}>{benefit.description}</p>
            </div>
          ))}
        </div>
      </div>
    </section>
  );
};

export default WhyChooseFitOn;
