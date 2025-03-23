"use client";
import { useState } from "react";
import styles from "./Faq.module.css";
import Header from "../Components/Header";
import Footer from "../Components/Footer";

export default function Faq() {
  // Manage the state for which accordion items are open
  const [openItems, setOpenItems] = useState({});

  // Toggle the open/closed state of an accordion item
  const toggleAccordion = (id) => {
    setOpenItems((prevState) => ({
      ...prevState,
      [id]: !prevState[id],
    }));
  };

  // FAQ content organized by sections
  const faqData = {
    general: [
      {
        id: "q1",
        question: "What is Fit-On?",
        answer:
          "Fit-On is an innovative fashion application that allows users to create accurate 3D models of their bodies to try on clothes virtually, helping them visualize how outfits will fit before purchasing. It also offers a digital wardrobe to organize clothing items and provides weather-based outfit recommendations.",
      },
      {
        id: "q2",
        question: "How does Fit-On work?",
        answer:
          "Fit-On uses LiDAR technology to generate a 3D model of your body from four images taken at different angles. Users can then try on digital versions of clothing items, mix and match outfits, and receive personalized fashion recommendations.",
      },
      {
        id: "q3",
        question: "What platforms does Fit-On support?",
        answer:
          "Fit-On is primarily developed for iOS devices with LiDAR capabilities (iPhone 12 Pro and above). A web platform is also available for users who prefer not to download an app.",
      },
      {
        id: "q4",
        question: "Is Fit-On a free app?",
        answer:
          "Fit-On offers a free version with basic features. However, premium features like advanced outfit recommendations, unlimited wardrobe storage, and retail integration may require a subscription.",
      },
    ],
    account: [
      {
        id: "q5",
        question: "How do I create my 3D model?",
        answer:
          "Simply take four photos of yourself from different angles using your iPhone. The app will process these images using LiDAR technology to create a precise 3D model of your body.",
      },
      {
        id: "q6",
        question: "What if I don't have an iPhone with LiDAR?",
        answer:
          "You can use our web platform to access basic features of Fit-On, such as digital wardrobe management and outfit planning. The 3D modeling feature, however, is exclusive to LiDAR-supported devices.",
      },
      {
        id: "q7",
        question: "Can I update my 3D model?",
        answer:
          "Yes, you can update your 3D model at any time by uploading new photos. Fit-On will automatically replace your previous model with the updated one.",
      },
    ],
    clothing: [
      {
        id: "q8",
        question: "How does Fit-On recommend outfits?",
        answer:
          "Fit-On analyzes your body measurements and matches them with available clothing items. It also considers weather conditions and your preferences to suggest the best outfits for you.",
      },
      {
        id: "q9",
        question: "Can I add my own clothes to the digital wardrobe?",
        answer:
          "Yes, you can upload photos of your clothing items, and Fit-On will add them to your digital wardrobe, allowing you to mix and match them with other outfits.",
      },
    ],
  };

  // Render an accordion item
  const AccordionItem = ({ id, question, answer }) => (
    <div className={styles.accordionItem}>
      <button
        className={styles.accordionButton}
        onClick={() => toggleAccordion(id)}
        aria-expanded={openItems[id] || false}
      >
        {question}
        <span
          className={`${styles.icon} ${openItems[id] ? styles.rotate : ""}`}
        >
          <svg
            xmlns="http://www.w3.org/2000/svg"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            strokeWidth="2"
            strokeLinecap="round"
            strokeLinejoin="round"
          >
            <line x1="12" y1="5" x2="12" y2="19"></line>
            <line x1="5" y1="12" x2="19" y2="12"></line>
          </svg>
        </span>
      </button>
      <div
        className={`${styles.accordionContent} ${
          openItems[id] ? styles.accordionContentOpen : ""
        }`}
      >
        <p className={styles.answer}>{answer}</p>
      </div>
    </div>
  );

  return (
    <>
      <Header />
      <div className={styles.faqContainer}>
        <div className={styles.faqMainContent}>
          <div className={styles.faqHeader}>
            <h1 className={styles.title}>Frequently Asked Questions</h1>
            <p className={styles.subtitle}>
              Find answers to common questions about Fit-On and how our virtual
              fitting technology works to enhance your shopping experience.
            </p>
          </div>

          <div className={styles.faqContent}>
            <div className={styles.faqSection}>
              <h2 className={styles.sectionTitle}>General Questions</h2>
              {faqData.general.map((item) => (
                <AccordionItem
                  key={item.id}
                  id={item.id}
                  question={item.question}
                  answer={item.answer}
                />
              ))}
            </div>

            <div className={styles.faqSection}>
              <h2 className={styles.sectionTitle}>Account & Setup</h2>
              {faqData.account.map((item) => (
                <AccordionItem
                  key={item.id}
                  id={item.id}
                  question={item.question}
                  answer={item.answer}
                />
              ))}
            </div>

            <div className={styles.faqSection}>
              <h2 className={styles.sectionTitle}>
                Clothing & Recommendations
              </h2>
              {faqData.clothing.map((item) => (
                <AccordionItem
                  key={item.id}
                  id={item.id}
                  question={item.question}
                  answer={item.answer}
                />
              ))}
            </div>
          </div>
        </div>
      </div>
      <Footer />
    </>
  );
}
